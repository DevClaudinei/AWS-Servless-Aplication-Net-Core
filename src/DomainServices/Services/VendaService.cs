using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using DomainModels;
using DomainModels.Entities;
using DomainModels.Enums;
using DomainServices.Exceptions;
using DomainServices.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace DomainServices.Services;

public class VendaService : IVendaService
{
    private readonly IAmazonDynamoDB _amazonDynamoDB;
    private readonly IOptions<DatabaseSettings> _databaseSettings;

    public VendaService(IAmazonDynamoDB amazonDynamoDB, IOptions<DatabaseSettings> databaseSettings)
    {
        if (databaseSettings is null)
        {
            throw new ArgumentNullException(nameof(databaseSettings));
        }
        _amazonDynamoDB = amazonDynamoDB;
        _databaseSettings = databaseSettings;
    }

    public async Task<string> CadastrarVenda(Venda venda)
    {
        if (venda.Status != Status.AguardandoPagamento)
            throw new BadRequestException($"Não é possível realizar uma compra com status: {venda.Status}.");

        VerificaItensEmVenda(venda);

        var createItemRequest = new PutItemRequest
        {
            TableName = _databaseSettings.Value.TableName,
            Item = FormataObjetoAPersistir(venda)
        };

        await _amazonDynamoDB.PutItemAsync(createItemRequest);

        return venda.Id;
    }

    private Dictionary<string, AttributeValue> FormataObjetoAPersistir(Venda venda)
    {
        var vendaEmJson = JsonSerializer.Serialize(venda);
        var itemDoDocumento = Document.FromJson(vendaEmJson);
        var itemComoAttributo = itemDoDocumento.ToAttributeMap();

        return itemComoAttributo;
    }

    private void VerificaItensEmVenda(Venda venda)
    {
        foreach (var item in venda.Itens)
        {
            if (item.Name is null)
                throw new BadRequestException($"Não é possível registrar a venda. Um nome de item não foi informado.");
            item.Id = Guid.NewGuid().ToString();
        }
    }

    public async Task<Venda> BuscarVendaPorId(string id)
    {
        var request = FormataObjetoABuscar(id);

        var response = await _amazonDynamoDB.GetItemAsync(request);

        if (response.Item.Count == 0) return null;
        
        var itemAsDocument = Document.FromAttributeMap(response.Item);

        return JsonSerializer.Deserialize<Venda>(itemAsDocument.ToJson());
    }

    private GetItemRequest FormataObjetoABuscar(string id)
    {
        var request = new GetItemRequest
        {
            TableName = _databaseSettings.Value.TableName,
            Key = new Dictionary<string, AttributeValue>
            {
                { "pk", new AttributeValue { S = id.ToString() } },
                { "sk", new AttributeValue { S = id.ToString() } }
            }
        };

        return request;
    }
    
    public void AtualizarVenda(string id, Venda venda)
    {
        var vendaEncontrada = BuscarVendaPorId(id);

        TransicaoDeStatus(vendaEncontrada.Result.Status, venda.Status);

        vendaEncontrada.Result.Status = venda.Status;

        var updateItemRequest = new PutItemRequest
        {
            TableName = _databaseSettings.Value.TableName,
            Item = FormataObjetoAPersistir(vendaEncontrada.Result)
        };

        _amazonDynamoDB.PutItemAsync(updateItemRequest);
    }

    private void TransicaoDeStatus(Status statusDaVenda, Status atualizacaoStatus)
    {
        switch (statusDaVenda)
        {
            case Status.AguardandoPagamento:
                if (!(atualizacaoStatus == Status.PagamentoAprovado || atualizacaoStatus == Status.Cancelada))
                    throw new BadRequestException($"O status: {statusDaVenda} não pode ser atualizado para o status: {atualizacaoStatus}.");
                break;
            case Status.PagamentoAprovado:
                if (!(atualizacaoStatus == Status.EnviadoParaTransportadora || atualizacaoStatus == Status.Cancelada))
                    throw new BadRequestException($"O status: {statusDaVenda} não pode ser atualizado para o status: {atualizacaoStatus}.");
                break;
            case Status.EnviadoParaTransportadora:
                if (atualizacaoStatus != Status.Entregue)
                    throw new BadRequestException($"O status: {statusDaVenda} não pode ser atualizado para o status: {atualizacaoStatus}.");
                break;
            case Status.Entregue:
                throw new BadRequestException($"O status da venda não pode ser atualizado, pois esta venda está com status: {statusDaVenda}.");
            default:
                throw new BadRequestException($"Status: {statusDaVenda} não existe.");
        }
    }
}