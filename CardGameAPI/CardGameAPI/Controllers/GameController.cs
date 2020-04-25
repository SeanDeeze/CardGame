using CardGameAPI.Hubs;
using CardGameAPI.Models;
using CardGameAPI.Models.Dto;
using CardGameAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace CardGameAPI.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class GameController : ControllerBase
  {
    private readonly GameRepository _gameRepository;

    public GameController(EFContext context, GameEngine gameEngine, IHubContext<GameHub> gameHub, ILogger<GameController> logger)
    {
      _gameRepository = new GameRepository(context, gameEngine, gameHub, logger);
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

    [HttpPost]
    public async System.Threading.Tasks.Task<CGMessage> StartGameAsync(Game game)
    {
      return await _gameRepository.StartGameAsync(game);
    }

    [HttpPost]
    public async System.Threading.Tasks.Task<CGMessage> EndGameAsync(Game game)
    {
      return await _gameRepository.EndGameAsync(game);
    }

    [HttpPost]
    public CGMessage IsPlayerInGame(Player p)
    {
      return _gameRepository.IsPlayerInGame(p);
    }
  }
}
