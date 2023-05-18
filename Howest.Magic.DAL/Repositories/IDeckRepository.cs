using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.DAL.Repositories
{
    internal interface IDeckRepository
    {
        Task<IQueryable<Card>> GetAllCards();
        void AddCard(Card card);
        void RemoveCard(Card card);

    }
}
