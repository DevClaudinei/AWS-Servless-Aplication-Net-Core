using DomainModels.Entities;
using DomainModels.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Application.Models;

public class CreateVendaRequest
{
    //public CreateVendaRequest(DateTime dataVenda, Vendedor vendedor, ICollection<Item> itens, Status status)
    //{
    //    DataVenda = dataVenda;
    //    Vendedor = vendedor;
    //    Itens = itens;
    //    Status = status;
    //}

    [JsonPropertyName("dataVenda")]
    public DateTime DataVenda { get; init; } = default!;

    [JsonPropertyName("vendedor")]
    public virtual Vendedor Vendedor { get; init; } = default!;

    [JsonPropertyName("itens")]
    public virtual ICollection<Item> Itens { get; init; } = default!;

    [JsonPropertyName("status")]
    public Status Status { get; init; } = default!;
}