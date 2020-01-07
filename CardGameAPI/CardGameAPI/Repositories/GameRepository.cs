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
      _logger = logger;
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
        _context.Games.Add(inputGame);
        _context.SaveChanges();
        inputGame.Cards = _gameEngine.GetCards();
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
        _context.Games.Remove(_gameEngine._games.Find(g => g.Id.Equals(inputGame.Id)));
        _context.SaveChanges();
        _gameEngine._games.Remove(_gameEngine._games.Find(g => g.Id.Equals(inputGame.Id)));
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
        Game game = _gameEngine._games.First(ge => ge.Id.Equals(playerGame.game.Id));
        Player p = _gameEngine._players.First(pl => pl.Id.Equals(playerGame.player.Id));
        if (game.Players.Find(pl => pl.Id.Equals(playerGame.player.Id)) == null)
        {
          game.Players.Add(playerGame.player);
          p.CurrentGame = game;
        }
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
        Game game = _gameEngine._games.First(ge => ge.Id.Equals(playerGame.game.Id));
        Player p = _gameEngine._players.First(pl => pl.Id.Equals(playerGame.player.Id));
        game.Players.RemoveAll(pl => pl.Id.Equals(playerGame.player.Id));
        p.CurrentGame = null;
        return GetGames();
      }
      catch (Exception ex)
      {
        _logger.Log(LogLevel.Error, $"Method:LeaveGame; Error: {ex.Message}", returnMessage);
      }
      return returnMessage;
    }

    public CGMessage StartGame(Game game)
    {
      CGMessage returnMessage = new CGMessage();
      try
      {
        _gameEngine.StartGameAsync(game.Id);
      }
      catch (Exception ex)
      {
        _logger.Log(LogLevel.Error, $"Method:LeaveGame; Error: {ex.Message}", returnMessage);
      }
      return returnMessage;
    }

    public CGMessage IsPlayerInGame(Player p)
    {
      CGMessage returnMessage = new CGMessage();
      try
      {
        foreach (Game gameEngineGame in _gameEngine._games)
        {
          Player currentGamePlayer =
            gameEngineGame.Players.FirstOrDefault(pl => pl.UserName.ToLower().Equals(p.UserName.ToLower()));
          if (currentGamePlayer != null) //Player found in active Game
          {
            returnMessage.ReturnData.Add(gameEngineGame);
          }
        }

        returnMessage.Status = true;
      }
      catch (Exception ex)
      {
        _logger.Log(LogLevel.Error, $"Method:LeaveGame; Error: {ex.Message}", returnMessage);
      }
      return returnMessage;
    }
  }
}
