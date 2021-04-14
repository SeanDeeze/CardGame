using System;
using CardGame.Controllers;
using CardGame.Models;
using CardGame.Models.dto;
using Microsoft.Extensions.Logging;

namespace CardGame.Repositories
{
  public class UserRepository
  {
    private readonly EFContext _context;
    private readonly IGameEngine _gameEngine;
    private readonly ILogger<UserController> _logger;

    public UserRepository(EFContext context, IGameEngine gameEngine, ILogger<UserController> logger)
    {
      _context = context;
      _gameEngine = gameEngine;
      _logger = logger;
    }

    public CGMessage GetUsers()
    {
      CGMessage returnMessage = new CGMessage();
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
