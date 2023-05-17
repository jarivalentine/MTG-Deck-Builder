using Microsoft.AspNetCore.Mvc;

namespace Howest.MagicCards.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardRepository _cardRepo;

        public CardsController()
        {
            _cardRepo = new SqlCardRepository();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Card>> GetCards()
        {
            return (_cardRepo.GetAllCards() is IEnumerable<Card> allCards)
                ? Ok(allCards)
                : NotFound("No cards found");
        }

        [HttpGet("{id:int}")]
        public ActionResult<Card> GetCard(int id)
        {
            return (_cardRepo.GetCardById(id) is Card foundCard)
                ? Ok(foundCard)
                : NotFound($"No book found with id {id}");
        }
    }
}
