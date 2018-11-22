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

    [HttpPut]
    public CGMessage SaveCard()
    {
      return _cardRepository.GetCards();
    }
  }
}
