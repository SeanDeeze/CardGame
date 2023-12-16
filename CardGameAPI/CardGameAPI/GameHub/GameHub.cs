using CardGame.Controllers;
using CardGame.Models;
using CardGame.Models.dto;
using CardGame.Repositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;

namespace CardGame.GameHub
{
    public class GameHub(EFContext context, ICoordinator gameEngine, ILogger<GameController> logger) : Hub
    {
        private readonly GameRepository _gameRepository = new GameRepository(context, gameEngine, logger);

        public async Task JoinGameConnection(Guid gameID)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gameID.ToString());

            string jsonResponse = JsonConvert.SerializeObject(_gameRepository.GetGameState(gameID));
            await Clients.Group(gameID.ToString()).SendAsync(jsonResponse);
        }

        public async Task RemoveGameConnection(Guid gameID)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gameID.ToString());

            string jsonResponse = JsonConvert.SerializeObject(_gameRepository.GetGameState(gameID));
            await Clients.Group(gameID.ToString()).SendAsync(jsonResponse);
        }

        public CGMessage GetGameState(Guid gameId)
        {
            return _gameRepository.GetGameState(gameId);
        }
    }
}
