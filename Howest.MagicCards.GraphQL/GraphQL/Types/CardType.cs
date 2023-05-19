using GraphQL.Types;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.DAL.Repositories;
using Howest.MagicCards.Shared.DTO;

namespace Howest.MagicCards.GraphQL.GraphQLTypes;

public class CardType : ObjectGraphType<CardReadDTO>
{
    public CardType()
    {
        Name = "Card";

        Field(c => c.Name, type: typeof(StringGraphType))
            .Description("Name of the card");
        Field(c => c.Text, type: typeof(StringGraphType))
            .Description("Text of the card");
    }
}
