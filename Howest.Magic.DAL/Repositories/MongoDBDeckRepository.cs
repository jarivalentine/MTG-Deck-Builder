using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Howest.MagicCards.DAL.Repositories;

public class MongoDBDeckRepository
{
    private readonly IMongoCollection<MongoDBCard> _cardsCollection;

    public MongoDBDeckRepository(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _cardsCollection = database.GetCollection<MongoDBCard>(mongoDBSettings.Value.CollectionName);
    }

    public async Task<List<MongoDBCard>> GetAsync()
    {
        return await _cardsCollection.Find(new BsonDocument()).ToListAsync();
    }
    public async Task CreateAsync(MongoDBCard card) 
    {
        await _cardsCollection.InsertOneAsync(card);
        return;
    }
    public async Task DeleteAsync(string id) { }
}
