namespace Howest.MagicCards.DAL.Repositories
{
    public interface IArtistRepository
    {
        Task<IQueryable<Artist>> GetAllArtists();
        Task<Artist?> GetArtistById(long id);
    }
}