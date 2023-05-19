using GraphQL;
using GraphQL.Types;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.DAL.Repositories;

namespace Howest.MagicCards.GraphQL.GraphQLTypes;

public class RootQuery : ObjectGraphType
{
    public RootQuery(ICardRepository cardRepository)
    {
        Name = "Query";

        FieldAsync<ListGraphType<CardType>>(
            "Cards",
            Description = "Get all cards",
            arguments: new QueryArguments
            {
                new QueryArgument<StringGraphType> { Name = "power" },
                new QueryArgument<StringGraphType> { Name = "toughness" },
            },
            resolve: async context =>
            {
                string power = context.GetArgument<string>("power");
                string toughness = context.GetArgument<string>("toughness");
                IQueryable<Card> cards = await cardRepository.GetAllCards();
                return cards
                    .Where(c => 
                        string.IsNullOrEmpty(power) || c.Power == power 
                        && string.IsNullOrEmpty(toughness) || c.Toughness == toughness
                    ).ToList();
            }
        );
    }
}
