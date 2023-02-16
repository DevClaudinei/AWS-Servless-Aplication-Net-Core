using Application.Models;
using AutoMapper;
using DomainModels.Entities;

public class VendaProfile : Profile
{
    public VendaProfile()
    {
        CreateMap<Venda, VendaResult>()
            .ForMember(x => x.Pk, opts => opts.MapFrom(source => source.Pk))
            .ForMember(x => x.Itens, opts => opts.MapFrom(source => source.Itens));
        CreateMap<UpdateVendaRequest, Venda>();
        CreateMap<CreateVendaRequest, Venda>();
            //.ForMember(x => x.Status, opts => opts.MapFrom(source => source.Status))
            //.ForMember(x => x.Itens, opts => opts.MapFrom(source => source.Itens));
    }
}