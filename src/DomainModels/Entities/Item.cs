using DomainModels.Interfaces;
using System.Text.Json.Serialization;

namespace DomainModels.Entities;

public class Item : IIdentifiable
{
    public Item() { }

    public Item(string name)
    {
        Name = name;
    }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get ; set ; }

}