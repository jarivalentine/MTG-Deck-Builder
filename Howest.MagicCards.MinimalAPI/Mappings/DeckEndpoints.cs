using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.DAL.Repositories;

namespace Howest.MagicCards.MinimalAPI.Mappings;

public static class DeckEndpoints
{
    public static void MapDeckEndpoints(this WebApplication app, string urlPrefix)
    {
        app.MapGet($"{urlPrefix}/deck", async (MongoDBDeckRepository deckRepo) =>
        {
            return (await deckRepo.GetAsync() is IEnumerable<MongoDBCard> cards)
                ? Results.Ok(cards)
                : Results.NotFound("No cards found");
        }).WithTags("Get all cards");

        app.MapPost($"{urlPrefix}/deck", async (MongoDBDeckRepository deckRepo, MongoDBCard newCard) =>
        {
            await deckRepo.CreateAsync(newCard);
            return Results.Created($"{urlPrefix}/deck/{newCard.Id}", newCard);
        }).Accepts<MongoDBCard>("application/json");
    }
}
