using Amazon.DynamoDBv2.DocumentModel;
using DomainModels.Enums;
using DomainModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DomainModels.Entities;

//[DynamoDBTable("vendas")]
public class Venda : IIdentifiable, ICreatable
{
    public Venda() { }

    public Venda(DateTime dataVenda, Vendedor vendedor, List<Item> itens, Status status)
    {
        DataVenda = dataVenda;
        Vendedor = vendedor;
        Itens = itens;
        Status = status;
    }

    [JsonPropertyName("pk")]
    public string Pk => Id;

    [JsonPropertyName("sk")]
    public string Sk => Pk;

    [JsonPropertyName("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [JsonPropertyName("dataVenda")]
    public DateTime DataVenda { get; set; }

    [JsonPropertyName("vendedor")]
    public virtual Vendedor Vendedor { get; set; }

    [JsonPropertyName("itens")]
    public List<Item> Itens { get; set; }

    [JsonPropertyName("status")]
    public Status Status { get; set; }
}