using CardGameAPI.Hubs;
using CardGameAPI.Models;
using CardGameAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CardGameAPI.Controllers
{
  [Route("api/[controller]/[action]")]
    [ApiController]
    public class GameController : ControllerBase
    {
    private readonly EFContext _context;
    private readonly GameEngine _gameEngine;
    private GameRepository _gameRepository;
    private readonly IHubContext<GameHub> _gameHub;

    public GameController(EFContext context, GameEngine gameEngine, IHubContext<GameHub> gameHub)
    {
      _context = context;
      _gameEngine = gameEngine;
      _gameHub = gameHub;
      _gameRepository = new GameRepository(_context, _gameEngine, _gameHub);
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

    [HttpPost]
    public CGMessage JoinGame(PlayerGame playerGame)
    {
      return _gameRepository.JoinGame(playerGame);
    }

    [HttpPost]
    public CGMessage LeaveGame(PlayerGame playerGame)
    {
      return _gameRepository.LeaveGame(playerGame);
    }
  }
}
