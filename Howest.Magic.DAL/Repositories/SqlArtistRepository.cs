using Microsoft.EntityFrameworkCore;

namespace Howest.MagicCards.DAL.Repositories
{
    public class SqlArtistRepository : IArtistRepository
    {
        private readonly MtgV1Context _db;

        public SqlArtistRepository(MtgV1Context db)
        {
            _db = db;
        }

        public async Task<IQueryable<Artist>> GetAllArtists()
        {
            IQueryable<Artist> allArtists = _db.Artists
                .Include(a => a.Cards)
                .Select(a => a);

            return await Task.FromResult(allArtists);
        }

        public async Task<Artist?> GetArtistById(long id)
        {
            Artist? singleArtist = await _db.Artists
                .Include(a => a.Cards)
                .SingleOrDefaultAsync(a => a.Id == id);

            return singleArtist;
        }
    }
}
