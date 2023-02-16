using DomainModels.Entities;
using DomainModels.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Application.Models;

public class VendaResult
{
    protected VendaResult() { }

    public VendaResult(string pk, DateTime dataVenda, List<Item> itens, Status status)
    {
        Pk = pk;
        DataVenda = dataVenda;
        Itens = itens;
        Status = status;
    }

    [JsonPropertyName("pk")]
    public string Pk { get; set; }

    [JsonPropertyName("dataVenda")]
    public DateTime DataVenda { get; set; }

    [JsonPropertyName("itens")]
    public List<Item> Itens { get; set; }

    [JsonPropertyName("status")]
    public Status Status { get; set; }
}