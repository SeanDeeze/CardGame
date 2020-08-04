using CardGameAPI.Controllers;
using CardGameAPI.Models;
using CardGameAPI.Models.Dto;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;

namespace CardGameAPI.Repositories
{
  public class UserRepository
  {
    private readonly EFContext _context;
    private readonly IGameEngine _gameEngine;
    private readonly ILogger<LoginController> _logger;

    public UserRepository(EFContext context, IGameEngine gameEngine, ILogger<LoginController> logger)
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
