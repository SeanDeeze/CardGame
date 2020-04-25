using CardGameAPI.Models;
using CardGameAPI.Models.Dto;
using CardGameAPI.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CardGameAPI.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class CardController : ControllerBase
  {
    private readonly CardRepository _cardRepository;

    public CardController(EFContext context, IHostingEnvironment hostingEnvironment)
    {
      _cardRepository = new CardRepository(context, hostingEnvironment);
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
