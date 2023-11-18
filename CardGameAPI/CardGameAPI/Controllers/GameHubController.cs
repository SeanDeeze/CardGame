using CardGame.Models;
using CardGame.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;

/* Implementation Guide: https://code-maze.com/netcore-signalr-angular-realtime-charts/ */
namespace CardGame.Controllers
{
    public class GameHubController(IHubContext<GameHub.GameHub> hub, EFContext context, ICoordinator gameEngine, ILogger<GameController> logger) : Controller
    {
        private readonly GameRepository _gameRepository = new GameRepository(context, gameEngine, logger);
        private readonly IHubContext<GameHub.GameHub> _hub = hub;

        [HttpGet]
        public IActionResult Get(Guid gameID)
        {
            _hub.Clients.All.SendAsync("GameState", _gameRepository.GetGameState(gameID));
            return Ok(new { Message = "Request Completed" });
        }
    }
}
