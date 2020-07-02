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

    public GameController(EFContext context, IGameEngine gameEngine, IHubContext<GameHub> gameHub, ILogger<GameController> logger)
    {
      _gameRepository = new GameRepository(context, gameEngine, gameHub, logger);
    }

    [HttpGet]
    public CGMessage GetGames()
    {
      return _gameRepository.GetGames();
    }

    [HttpPost]
    public CGMessage GetGameState([FromBody]int gameId)
    {
      return _gameRepository.GetGameState(gameId);
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
    public CGMessage StartGame(Game game)
    {
      return _gameRepository.StartGame(game);
    }

    [HttpPost]
    public CGMessage EndGame(Game game)
    {
      return _gameRepository.EndGame(game);
    }

    [HttpPost]
    public CGMessage IsPlayerInGame(Player p)
    {
      return _gameRepository.IsPlayerInGame(p);
    }
  }
}
