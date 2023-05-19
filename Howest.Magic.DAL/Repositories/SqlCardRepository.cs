using Microsoft.EntityFrameworkCore;

namespace Howest.MagicCards.DAL.Repositories
{
    public class SqlCardRepository : ICardRepository
    {
        private readonly MtgV1Context _db;

        public SqlCardRepository(MtgV1Context db)
        {
            _db = db;
        }

        public async Task<IQueryable<Card>> GetAllCards()
        {
            IQueryable<Card> allCards = _db.Cards
                .Include(c => c.Artist)
                .Select(c => c);

            return await Task.FromResult(allCards);
        }

        public async Task<Card?> GetCardById(long id)
        {
            Card? singleCard = await _db.Cards.SingleOrDefaultAsync(c => c.Id == id);

            return singleCard;
        }
    }
}
