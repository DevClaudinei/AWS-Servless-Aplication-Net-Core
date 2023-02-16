using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DomainModels.Interfaces;

public interface IIdentifiable
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
}