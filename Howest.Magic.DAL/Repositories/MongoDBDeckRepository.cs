using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Howest.MagicCards.DAL.Repositories;

public class MongoDBDeckRepository : IDeckRepository
{
    private readonly IMongoCollection<MongoDBCard> _cardsCollection;

    public MongoDBDeckRepository(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _cardsCollection = database.GetCollection<MongoDBCard>(mongoDBSettings.Value.CollectionName);
    }

    public async Task<List<MongoDBCard>> GetAllCards()
    {
        return await _cardsCollection.Find(new BsonDocument()).ToListAsync();
    }
    public async Task CreateCard(MongoDBCard card) 
    {
        await _cardsCollection.InsertOneAsync(card);
        return;
    }
    public async Task DeleteCards() 
    {
        FilterDefinition<MongoDBCard> filter = Builders<MongoDBCard>.Filter.Empty;
        await _cardsCollection.DeleteManyAsync(filter);
        return;
    }

    public async Task PutCardAmount(long id, int amount) 
    {
        FilterDefinition<MongoDBCard> filter = Builders<MongoDBCard>.Filter.Eq("Id", id);
        if (amount <= 0)
        {
            await _cardsCollection.DeleteOneAsync(filter);
            return;
        }
        UpdateDefinition<MongoDBCard> update = Builders<MongoDBCard>.Update.Set<int>("amount", amount);
        await _cardsCollection.UpdateOneAsync(filter, update);
        return;
    }
}
