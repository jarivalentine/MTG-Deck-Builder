using GraphQL;
using GraphQL.Types;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.DAL.Repositories;
using Howest.MagicCards.GraphQL.GraphQL.Types;
using Microsoft.IdentityModel.Tokens;

namespace Howest.MagicCards.GraphQL.GraphQLTypes;

public class RootQuery : ObjectGraphType
{
    public RootQuery(ICardRepository cardRepository, IArtistRepository artistRepository)
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
                Console.WriteLine($"power: {power}, toughness: {toughness}");
                IQueryable<Card> cards = await cardRepository.GetAllCards();
                return cards
                    .Where(c => 
                        string.IsNullOrEmpty(power) || c.Power == power 
                        && string.IsNullOrEmpty(toughness) || c.Toughness == toughness
                    ).ToList();
            }
        );

        FieldAsync<ListGraphType<ArtistType>>(
            "Artists",
            Description = "Get all artists",
            arguments: new QueryArguments
            {
                new QueryArgument<IntGraphType> { Name = "limit" },
            },
            resolve: async context =>
            {
                int limit = context.GetArgument<int>("limit");
                IQueryable<Artist> artists = await artistRepository.GetAllArtists();
                if (limit > 0)
                {
                    artists = artists.Take(limit);
                }
                return artists.ToList();
            }
        );

        FieldAsync<ArtistType>(
            "Artist",
            Description = "Get all artists",
            arguments: new QueryArguments
            {
                new QueryArgument<LongGraphType> { Name = "id" },
            },
            resolve: async context =>
            {
                long id = context.GetArgument<long>("id");
                Artist artists = await artistRepository.GetArtistById(id);
                return artists;
            }
        );
    }
}
