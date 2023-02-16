using DomainModels.Interfaces;
using System;
using System.Text.Json.Serialization;

namespace DomainModels.Entities;

public class Vendedor : IIdentifiable
{
    public Vendedor() { }

    public Vendedor(string cpf, string nome, string telefone)
    {
        Cpf = cpf;
        Nome = nome;
        Telefone = telefone;
    }

    [JsonPropertyName("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [JsonPropertyName("cpf")]
    public string Cpf { get; set; }

    [JsonPropertyName("nome")]
    public string Nome { get; set; }

    [JsonPropertyName("telefone")]
    public string Telefone { get; set; }
}