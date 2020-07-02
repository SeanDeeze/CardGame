using CardGameAPI.Controllers;
using CardGameAPI.Hubs;
using CardGameAPI.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardGameAPI.Models.Dto;

namespace CardGameAPI.Repositories
{
  public class UserRepository
  {
    private readonly EFContext _context;
    private readonly IGameEngine _gameEngine;
    private readonly ILogger<LoginController> _logger;
    private readonly IHubContext<GameHub> _gameHub;

    public UserRepository(EFContext context, IGameEngine gameEngine, IHubContext<GameHub> gameHub, ILogger<LoginController> logger)
    {
      _context = context;
      _gameEngine = gameEngine;
      _logger = logger;
      _gameHub = gameHub;
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
