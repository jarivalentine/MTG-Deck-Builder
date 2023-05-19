using GraphQL;
using GraphQL.Types;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.DAL.Repositories;
using Howest.MagicCards.GraphQL.GraphQL.Types;
using Howest.MagicCards.Shared.DTO;

namespace Howest.MagicCards.GraphQL.GraphQLTypes;

public class CardType : ObjectGraphType<Card>
{
    public CardType()
    {
        Name = "Card";

        Field(c => c.Name, type: typeof(StringGraphType));
        Field(c => c.Type, type: typeof(StringGraphType));
        Field(c => c.Text, type: typeof(StringGraphType));
        Field(c => c.ManaCost, type: typeof(StringGraphType));
        Field(c => c.Power, type: typeof(StringGraphType));
        Field(c => c.Toughness, type: typeof(StringGraphType));
        Field(c => c.Artist, type: typeof(ArtistType));
    }
}
