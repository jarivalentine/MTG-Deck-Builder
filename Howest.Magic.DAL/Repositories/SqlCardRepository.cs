using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Scaffold-DbContext “Server=localhost;Initial Catalog=mtg_v1;Integrated Security=True;Encrypt=False” -Project Howest.MagicCards.DAL Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

namespace Howest.MagicCards.DAL.Repositories
{
    public class SqlCardRepository : ICardRepository
    {
        private readonly MtgV1Context _db;

        public SqlCardRepository()
        {
            _db = new MtgV1Context();
        }

        public IEnumerable<Card> GetAllCards()
        {
            IQueryable<Card> allCards = _db.Cards.Take(10).Select(c => c);

            return allCards.ToList();
        }

        public Card? GetCardById(int id)
        {
            Card? singleCard = _db.Cards.SingleOrDefault(c => c.Id == id);

            return singleCard;
        }
    }
}
