using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.DAL.Repositories;

namespace Howest.MagicCards.MinimalAPI.Mappings;

public static class DeckEndpoints
{
    public static void MapDeckEndpoints(this WebApplication app, string urlPrefix)
    {
        app.MapGet($"{urlPrefix}/deck", async (MongoDBDeckRepository deckRepo) =>
        {
            return (await deckRepo.GetAllCards() is IEnumerable<MongoDBCard> cards)
                ? Results.Ok(cards)
                : Results.NotFound("No cards found");
        }).WithTags("Deck");

        app.MapPost($"{urlPrefix}/deck", async (MongoDBDeckRepository deckRepo, MongoDBCard newCard) =>
        {
            await deckRepo.CreateCard(newCard);
            return Results.Created($"{urlPrefix}/deck/{newCard.Id}", newCard);
        }).Accepts<MongoDBCard>("application/json").WithTags("Deck"); ;

        app.MapDelete($"{urlPrefix}/deck/{{id}}", async (MongoDBDeckRepository deckRepo, long id) =>
        {
            await deckRepo.DeleteCard(id);
            return Results.Ok($"Card with id {id} is deleted!");
        }).WithTags("Deck"); ;

        app.MapPut($"{urlPrefix}/students/{{id}}/{{amount}}", async (MongoDBDeckRepository deckRepo, long id, int amount) =>
        {
            await deckRepo.PutCardAmount(id, amount);
            return Results.Ok($"Card with id {id} updated to amount {amount}");
        }).WithTags("Deck"); ;
    }

    public static void AddDeckServices(this IServiceCollection services)
    {
        services.AddSingleton<MongoDBDeckRepository>();
    }
}
