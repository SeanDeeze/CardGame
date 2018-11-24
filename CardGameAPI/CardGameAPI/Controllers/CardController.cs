using CardGameAPI.Models;
using CardGameAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CardGameAPI.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class CardController : ControllerBase
  {
    private readonly EFContext _context;
    private CardRepository _cardRepository;

    public CardController(EFContext context)
    {
      _context = context;
      _cardRepository = new CardRepository(_context);
    }

    [HttpPost]
    public CGMessage GetCards()
    {
      return _cardRepository.GetCards();
    }

    [HttpPost]
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
