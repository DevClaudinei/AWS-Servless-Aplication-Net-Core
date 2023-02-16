using Application.Models;
using AppServices.Services.Interfaces;
using AutoMapper;
using DomainModels.Entities;
using DomainServices.Exceptions;
using DomainServices.Interfaces;
using System;
using System.Threading.Tasks;

namespace AppServices.Services;

public class VendaAppService : IVendaAppService
{
    private readonly IVendaService _vendaService;
    private readonly IMapper _mapper;

    public VendaAppService(IVendaService vendaService, IMapper mapper)
    {
        _vendaService = vendaService ?? throw new ArgumentNullException(nameof(vendaService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public Task<string> CadastrarVenda(CreateVendaRequest createVendaRequest)
    {
        var vendaMap = _mapper.Map<Venda>(createVendaRequest);

        return _vendaService.CadastrarVenda(vendaMap);
    }

    public async Task<VendaResult> BuscarVendaPorId(string id)
    {
        var vendaFound = await _vendaService.BuscarVendaPorId(id) ??
            throw new NotFoundException($"Venda para o Id: {id} não encontrada.");

        //vendaFound.Id = id.ToString();

        return _mapper.Map<VendaResult>(vendaFound);
    }

    public void AtualizarVenda(string id, UpdateVendaRequest updateVendaRequest)
    {
        var vendaMap = _mapper.Map<Venda>(updateVendaRequest);

        _vendaService.AtualizarVenda(id, vendaMap);
    }
}