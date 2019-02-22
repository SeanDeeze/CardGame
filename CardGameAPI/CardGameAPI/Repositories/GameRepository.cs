using CardGameAPI.Controllers;
using CardGameAPI.Hubs;
using CardGameAPI.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGameAPI.Repositories
{
  public class GameRepository
  {
    private ILogger<GameController> _logger;
    private EFContext _context;
    private GameEngine _gameEngine;
    private IHubContext<GameHub> _gameHub;
    public GameRepository(EFContext context, GameEngine gameEngine, IHubContext<GameHub> gameHub, ILogger<GameController> logger)
    {
      _context = context;
      _gameEngine = gameEngine;
      _gameHub = gameHub;
    }

    public CGMessage GetGames()
    {
      CGMessage returnMessage = new CGMessage();
      try
      {
        List<Game> games = _gameEngine._games.ToList();
        returnMessage.ReturnData.Add(games);
        returnMessage.Status = true;
        _gameHub.Clients.All.SendAsync("ReceiveGames", games);
      }
      catch (Exception ex)
      {
        _logger.Log(LogLevel.Error, $"Method:GameGames; Error: {ex.Message}", returnMessage);
      }
      return returnMessage;
    }

    public CGMessage SaveGame(Game inputGame)
    {
      CGMessage returnMessage = new CGMessage();
      try
      {
        _gameEngine._games.Add(inputGame);
        return GetGames();
      }
      catch (Exception ex)
      {
        _logger.Log(LogLevel.Error, $"Method:SaveGame; Error: {ex.Message}", returnMessage);
      }
      return returnMessage;
    }

    public CGMessage DeleteGame(Game inputGame)
    {
      CGMessage returnMessage = new CGMessage();
      try
      {
        _gameEngine._games.Remove(_gameEngine._games.Find(g => g.Id == inputGame.Id));
        return GetGames();
      }
      catch (Exception ex)
      {
        _logger.Log(LogLevel.Error, $"Method:DeleteGame; Error: {ex.Message}", returnMessage);
      }
      return returnMessage;
    }

    public CGMessage JoinGame(PlayerGame playerGame)
    {
      CGMessage returnMessage = new CGMessage();
      try
      {
        Game game = _gameEngine._games.First(ge => ge.Id == playerGame.game.Id);
        game.Players.Add(playerGame.player);
        return GetGames();
      }
      catch (Exception ex)
      {
        _logger.Log(LogLevel.Error, $"Method:JoinGame; Error: {ex.Message}", returnMessage);
      }
      return returnMessage;
    }

    public CGMessage LeaveGame(PlayerGame playerGame)
    {
      CGMessage returnMessage = new CGMessage();
      try
      {
        Game game = _gameEngine._games.First(ge => ge.Id == playerGame.game.Id);
        game.Players.Remove(playerGame.player);
        return GetGames();
      }
      catch (Exception ex)
      {
        _logger.Log(LogLevel.Error, $"Method:LeaveGame; Error: {ex.Message}", returnMessage);
      }
      return returnMessage;
    }
  }
}
