using AutoMapper;
using Howest.MagicCards.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using AutoMapper.QueryableExtensions;
using Howest.MagicCards.WebAPI.Wrappers;
using Howest.MagicCards.Shared.Filters;

namespace Howest.MagicCards.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardRepository _cardRepo;
        private readonly IMapper _mapper;

        public CardsController(ICardRepository cardRepo, IMapper mapper)
        {
            _cardRepo = cardRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<PagedResponse<IEnumerable<CardReadDTO>>> GetCards([FromQuery] PaginationFilter paginationFilter)
        {
            return (_cardRepo.GetAllCards() is IQueryable<Card> allCards)
                ? Ok(new PagedResponse<IEnumerable<CardReadDTO>>(
                        allCards
                            .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                            .Take(paginationFilter.PageSize)
                            .ProjectTo<CardReadDTO>(_mapper.ConfigurationProvider)
                            .ToList(),
                        paginationFilter.PageNumber,
                        paginationFilter.PageSize
                    )
                )
                : NotFound(
                    new Response<CardReadDTO>
                    {
                        Errors = new string[] { "404" },
                        Message = "No cards found"
                    }
                );
        }

        [HttpGet("{id:int}")]
        public ActionResult<Response<CardReadDTO>> GetCard(int id)
        {
            return (_cardRepo.GetCardById(id) is Card foundCard)
                ? Ok(new Response<CardReadDTO>(_mapper.Map<CardReadDTO>(foundCard)))
                : NotFound(
                    new Response<CardReadDTO>()
                    {
                        Errors = new string[] { "404" },
                        Message = $"No card found with id {id}"
                    }    
                );
        }
    }
}
