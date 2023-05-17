using AutoMapper;
using Howest.MagicCards.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.Shared.Mappings
{
    public class CardsProfile : Profile
    {
        public CardsProfile()
        {
            CreateMap<Card, CardReadDTO>();
        }
    }
}
