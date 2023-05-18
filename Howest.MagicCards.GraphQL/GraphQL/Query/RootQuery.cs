using GraphQL.Types;
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
            resolve: async context =>
            {
                return await cardRepository.GetAllCards();
            }
        );
    }
}
