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

        public IQueryable<Card> GetAllCards()
        {
            IQueryable<Card> allCards = _db.Cards.Take(10).Select(c => c);

            return allCards;
        }

        public Card? GetCardById(int id)
        {
            Card? singleCard = _db.Cards.SingleOrDefault(c => c.Id == id);

            return singleCard;
        }
    }
}
