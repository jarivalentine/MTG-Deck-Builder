using AutoMapper;
using Howest.MagicCards.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using AutoMapper.QueryableExtensions;

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
        public ActionResult<IEnumerable<CardReadDTO>> GetCards()
        {
            return (_cardRepo.GetAllCards() is IQueryable<Card> allCards)
                ? Ok(allCards.ProjectTo<CardReadDTO>(_mapper.ConfigurationProvider).ToList())
                : NotFound("No cards found");
        }

        [HttpGet("{id:int}")]
        public ActionResult<CardReadDTO> GetCard(int id)
        {
            return (_cardRepo.GetCardById(id) is Card foundCard)
                ? Ok(_mapper.Map<CardReadDTO>(foundCard))
                : NotFound($"No book found with id {id}");
        }
    }
}
