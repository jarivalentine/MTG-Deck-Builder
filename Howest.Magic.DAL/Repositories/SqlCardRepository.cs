using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            IQueryable<Card> allCards = _db.Cards.Select(c => c);

            return await Task.FromResult(allCards);
        }

        public async Task<Card?> GetCardById(int id)
        {
            Card? singleCard = await _db.Cards.SingleOrDefaultAsync(c => c.Id == id);

            return singleCard;
        }
    }
}
