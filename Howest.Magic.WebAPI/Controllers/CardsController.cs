using AutoMapper;
using Howest.MagicCards.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using AutoMapper.QueryableExtensions;
using Howest.MagicCards.WebAPI.Wrappers;
using Howest.MagicCards.Shared.Filters;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Howest.MagicCards.Shared.Extensions;
using Microsoft.Extensions.Caching.Memory;

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
        private readonly IMemoryCache _cache;

        public CardsController(ICardRepository cardRepo, IMapper mapper, IMemoryCache memoryCache)
        {
            _cardRepo = cardRepo;
            _mapper = mapper;
            _cache = memoryCache;
        }

        [MapToApiVersion("1.1")]
        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<CardReadDTO>>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<ActionResult<PagedResponse<IEnumerable<CardReadDTO>>>> GetCards([FromQuery] CardFilter filter, [FromServices] IConfiguration config)
        {
            try
            {
                return (await _cardRepo.GetAllCards() is IQueryable<Card> allCards)
                    ? Ok(new PagedResponse<IEnumerable<CardReadDTO>>(
                            await allCards
                                .Where(c => 
                                    c.Name.Contains(filter.Name) 
                                    && c.Type.Contains(filter.Type) 
                                    && c.Text.Contains(filter.Text)
                                    && c.Artist.FullName.Contains(filter.Artist)
                                )
                                .Where(c =>
                                    filter.Rarity.IsNullOrEmpty() || c.RarityCodeNavigation.Name == filter.Rarity
                                    && filter.Set.IsNullOrEmpty() || c.SetCodeNavigation.Name == filter.Set)
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
        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<CardReadDTO>>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<ActionResult<PagedResponse<IEnumerable<CardReadDTO>>>> GetCards([FromQuery] CardOrderFilter filter, [FromServices] IConfiguration config)
        {
            try
            {
                return (await _cardRepo.GetAllCards() is IQueryable<Card> allCards)
                    ? Ok(new PagedResponse<IEnumerable<CardReadDTO>>(
                            await allCards
                                .Where(c => c.Name.Contains(filter.Name) && c.Type.Contains(filter.Type) && c.Text.Contains(filter.Text))
                                .Sort(filter.OrderBy ?? string.Empty)
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

        [HttpGet("rarities")]
        [ProducesResponseType(typeof(Response<IEnumerable<string>>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<ActionResult<Response<IEnumerable<string>>>> GetRarities()
        {
            try
            {
                return (await _cardRepo.GetAllRarities() is IQueryable<Rarity> allRarities)
                    ? Ok(new Response<IEnumerable<string>>(
                            await allRarities
                                .Select(r => r.Name)
                                .ToListAsync()
                            )
                        )
                    : NotFound(
                        new Response<IEnumerable<string>>
                        {
                            Succeeded = false,
                            Errors = new string[] { $"Status code: {StatusCodes.Status404NotFound}" },
                            Message = "No rarities found"
                        }
                    );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new Response<IEnumerable<string>>()
                    {
                        Succeeded = false,
                        Errors = new string[] { $"Status code: {StatusCodes.Status500InternalServerError}" },
                        Message = $"({ex.Message}) "
                    });
            }
        }

        [HttpGet("sets")]
        [ProducesResponseType(typeof(Response<IEnumerable<string>>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<ActionResult<Response<IEnumerable<string>>>> GetSets()
        {
            try
            {
                return (await _cardRepo.GetAllSets() is IQueryable<Set> allSets)
                    ? Ok(new Response<IEnumerable<string>>(
                            await allSets
                                .Select(s => s.Name)
                                .ToListAsync()
                            )
                        )
                    : NotFound(
                        new Response<IEnumerable<string>>
                        {
                            Succeeded = false,
                            Errors = new string[] { $"Status code: {StatusCodes.Status404NotFound}" },
                            Message = "No sets found"
                        }
                    );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new Response<IEnumerable<string>>()
                    {
                        Succeeded = false,
                        Errors = new string[] { $"Status code: {StatusCodes.Status500InternalServerError}" },
                        Message = $"({ex.Message}) "
                    });
            }
        }

        [MapToApiVersion("1.5")]
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Response<CardReadDTO>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<ActionResult<Response<CardReadDTO>>> GetCard(int id)
        {
            try
            {
                if (!_cache.TryGetValue(id, out CardReadDTO cachedResult))
                {
                    cachedResult = _mapper.Map<CardReadDTO>(await _cardRepo.GetCardById(id));

                    MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                    };

                    _cache.Set(id, cachedResult, cacheOptions);
                }

                return Ok(new Response<CardReadDTO>(cachedResult));
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