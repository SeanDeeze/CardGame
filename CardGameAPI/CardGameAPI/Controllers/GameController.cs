using CardGameAPI.Models;
using CardGameAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CardGameAPI.Controllers
{
  [Route("api/[controller]/[action]")]
    [ApiController]
    public class GameController : ControllerBase
    {

    private readonly EFContext _context;
    private GameRepository _gameRepository;

    public GameController(EFContext context)
    {
      _context = context;
      _gameRepository = new GameRepository(_context);
    }

    [HttpPost]
    public CGMessage GetGames()
    {
      return _gameRepository.GetGames();
    }

    [HttpPut]
    public CGMessage SaveGame(Game game)
    {
      return _gameRepository.SaveGame(game);
    }

    [HttpPost]
    public CGMessage DeleteGame(Game game)
    {
      return _gameRepository.DeleteGame(game);
    }
  }
}
