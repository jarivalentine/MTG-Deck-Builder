using GraphQL.Types;
using Howest.MagicCards.DAL.Models;

namespace Howest.MagicCards.GraphQL.GraphQL.Types;

public class ArtistType : ObjectGraphType<Artist>
{
    public ArtistType()
    {
        Name = "Artist";

        Field(a => a.Id, type: typeof(LongGraphType));
        Field(a => a.FullName, type: typeof(StringGraphType));
    }

}
