using CardGame.Models;
using CardGame.Models.dto;
using CardGame.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CardGame.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly CardRepository _cardRepository;

        public CardController(EFContext context)
        {
            _cardRepository = new CardRepository(context);
        }

        [HttpGet]
        public CGMessage GetCards()
        {
            return _cardRepository.GetCards();
        }

        [HttpGet]
        public CGMessage GetCardRoles()
        {
            return _cardRepository.GetCardRoles();
        }

        [HttpPut]
        public CGMessage SaveCard(Card card)
        {
            return _cardRepository.SaveCard(card);
        }

        [HttpPut]
        public CGMessage SaveCardRole(CardRole cardRole)
        {
            return _cardRepository.SaveCardRole(cardRole);
        }

        [HttpPost]
        public CGMessage DeleteCard(Card card)
        {
            return _cardRepository.DeleteCard(card);
        }

        [HttpPost]
        public CGMessage DeleteCardRole(CardRole cardRole)
        {
            return _cardRepository.DeleteCardRole(cardRole);
        }
    }
}
