using CardGame.Models;
using CardGame.Models.dto;
using CardGame.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CardGame.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly GameRepository _gameRepository;

        public GameController(EFContext context, IGameEngine gameEngine, ILogger<GameController> logger)
        {
            _gameRepository = new GameRepository(context, gameEngine, logger);
        }

        [HttpGet]
        public CGMessage GetGames()
        {
            return _gameRepository.GetGames();
        }

        [HttpPost]
        public CGMessage GetGameState(Game game)
        {
            return _gameRepository.GetGameState(game);
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
    }
}
