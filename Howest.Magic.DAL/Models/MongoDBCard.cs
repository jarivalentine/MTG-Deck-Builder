using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace Howest.MagicCards.DAL.Models;

public class MongoDBCard
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string mongo_id { get; set; }

    [BsonElement("id")]
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [BsonElement("amount")]
    [JsonPropertyName("amount")]
    public int Amount { get; set; } = 1;

    [BsonElement("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [BsonElement("mana_cost")]
    [JsonPropertyName("mana_cost")]
    public string? ManaCost { get; set; }
}
