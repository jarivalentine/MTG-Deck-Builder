using System.Text.Json.Serialization;

namespace Howest.MagicCards.DAL.Models;

public class MongoDBAmount
{
    [JsonPropertyName("amount")]
    public int Amount { get; set; } = 1;
}
