using Microsoft.AspNetCore.Mvc;

namespace Howest.MagicCards.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly CardRepository _cardRepo;

        public CardsController(CardRepository cardRepo)
        {
            _cardRepo = cardRepo;
        }

        [HttpGet]
        public IActionResult GetCards()
        {
            return (_cardRepo.GetCards() is IEnumerable<string> cards)
                ? Ok(cards)
                : NotFound("No cards found");
        }
    }
}
