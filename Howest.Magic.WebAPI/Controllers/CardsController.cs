using AutoMapper;
using Howest.MagicCards.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using AutoMapper.QueryableExtensions;
using Howest.MagicCards.WebAPI.Wrappers;
using Howest.MagicCards.Shared.Filters;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace Howest.MagicCards.WebAPI.Controllers
{
    [ApiVersion("1.1")]
    [ApiVersion("1.5")]
    [Route("api/v{version:apiVersion}/[controller]")]
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
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<CardReadDTO>>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<ActionResult<PagedResponse<IEnumerable<CardReadDTO>>>> GetCards([FromQuery] CardFilter filter)
        {
            try
            {
                return (await _cardRepo.GetAllCards() is IQueryable<Card> allCards)
                    ? Ok(new PagedResponse<IEnumerable<CardReadDTO>>(
                            await allCards
                                .Where(c => string.IsNullOrEmpty(filter.Name) || c.Name.Contains(filter.Name))
                                .Skip((filter.PageNumber - 1) * filter.PageSize)
                                .Take(filter.PageSize)
                                .ProjectTo<CardReadDTO>(_mapper.ConfigurationProvider)
                                .ToListAsync(),
                            filter.PageNumber,
                            filter.PageSize
                        )
                    )
                    : NotFound(
                        new Response<CardReadDTO>
                        {
                            Succeeded = false,
                            Errors = new string[] { $"Status code: {StatusCodes.Status404NotFound}" },
                            Message = "No cards found"
                        }
                    );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new Response<CardReadDTO>()
                    {
                        Succeeded = false,
                        Errors = new string[] { $"Status code: {StatusCodes.Status500InternalServerError}" },
                        Message = $"({ex.Message}) "
                    }
                );
            }
        }

        [MapToApiVersion("1.5")]
        [HttpGet("{id:int}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<Response<CardReadDTO>>> GetCard(int id)
        {
            try
            {
                return (await _cardRepo.GetCardById(id) is Card foundCard)
                    ? Ok(new Response<CardReadDTO>(_mapper.Map<CardReadDTO>(foundCard)))
                    : NotFound(
                        new Response<CardReadDTO>()
                        {
                            Succeeded = false,
                            Errors = new string[] { $"Status code: {StatusCodes.Status404NotFound}" },
                            Message = $"No card found with id {id}"
                        }
                    );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new Response<CardReadDTO>()
                    {
                        Succeeded = false,
                        Errors = new string[] { $"Status code: {StatusCodes.Status500InternalServerError}" },
                        Message = $"({ex.Message}) "
                    }
                );
            }
        }
    }
}
