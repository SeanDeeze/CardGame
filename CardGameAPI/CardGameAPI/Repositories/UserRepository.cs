using System;
using CardGame.Controllers;
using CardGame.Models;
using CardGame.Models.dto;
using Microsoft.Extensions.Logging;

namespace CardGame.Repositories
{
    public class UserRepository(EFContext context, ICoordinator gameEngine, ILogger<UserController> logger)
    {
        private readonly EFContext _context = context;
        private readonly ICoordinator _gameEngine = gameEngine;
        private readonly ILogger<UserController> _logger = logger;

        public CGMessage GetUsers()
        {
            CGMessage returnMessage = new();
            try
            {
                returnMessage.ReturnData.Add(_gameEngine.GetPlayers());
                returnMessage.Status = true;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Method:Login; Error: {ex.Message}", returnMessage);
            }
            return returnMessage;
        }

    }
}
