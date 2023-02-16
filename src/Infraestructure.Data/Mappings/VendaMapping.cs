using DomainModels.Entities;
using MongoDB.Bson.Serialization;
using MongoDB.Generators;
using MongoDB.Infrastructure;

namespace Infraestructure.Data.Mappings;

public class VendaMapping : IMongoDbFluentConfiguration
{
    public void Configure()
{
    if (BsonClassMap.IsClassMapRegistered(typeof(Venda)))
    {
        return;
    }

    BsonClassMap.RegisterClassMap<Venda>(builder =>
    {
        builder.AutoMap();
        builder.MapIdMember(x => x.Id).SetIdGenerator(Int64IdGenerator<Venda>.Instance);
    });
}
}