using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.DAL.Repositories
{
    public class CardRepository : ICardRepository
    {
        private List<string> _cards = new List<string> { "Card1", "Card2" };

        public IEnumerable<string> GetCards()
        {
            return _cards;
        }
    }
}
