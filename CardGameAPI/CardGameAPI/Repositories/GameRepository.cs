using CardGameAPI.Controllers;
using CardGameAPI.Hubs;
using CardGameAPI.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using CardGameAPI.Models.Dto;

namespace CardGameAPI.Repositories
{
  public class GameRepository
  {
    private readonly ILogger<GameController> _logger;
    private readonly EFContext _context;
    private readonly GameEngine _gameEngine;
    private readonly IHubContext<GameHub> _gameHub;
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
        List<Game> games = _gameEngine.Games.ToList();
        returnMessage.ReturnData.Add(games);
        _gameHub.Clients.All.SendAsync("ReceiveGames", games);
        returnMessage.Status = true;
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
        _gameEngine.Games.Add(inputGame);
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
        _context.Games.Remove(_gameEngine.Games.Find(g => g.Id.Equals(inputGame.Id)));
        _context.SaveChanges();
        _gameEngine.Games.Remove(_gameEngine.Games.Find(g => g.Id.Equals(inputGame.Id)));
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
        Game game = _gameEngine.Games.First(ge => ge.Id.Equals(playerGame.Game.Id));
        Player p = _gameEngine.Players.First(pl => pl.Id.Equals(playerGame.Player.Id));
        if (game.Players.Find(pl => pl.Id.Equals(playerGame.Player.Id)) == null)
        {
          game.Players.Add(playerGame.Player);
        }
        p.CurrentGame = game;
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
        Game game = _gameEngine.Games.First(ge => ge.Id.Equals(playerGame.Game.Id));
        Player p = _gameEngine.Players.First(pl => pl.Id.Equals(playerGame.Player.Id));
        game.Players.RemoveAll(pl => pl.Id.Equals(playerGame.Player.Id));
        p.CurrentGame = null;
        return GetGames();
      }
      catch (Exception ex)
      {
        _logger.Log(LogLevel.Error, $"Method:LeaveGame; Error: {ex.Message}", returnMessage);
      }
      return returnMessage;
    }

    public async System.Threading.Tasks.Task<CGMessage> StartGameAsync(Game game)
    {
      CGMessage returnMessage = new CGMessage();
      try
      {
        await _gameEngine.StartGameAsync(game.Id, _gameHub);
        returnMessage.Status = true;
      }
      catch (Exception ex)
      {
        _logger.Log(LogLevel.Error, $"Method:StartGameAsync; Error: {ex.Message}", returnMessage);
      }
      return returnMessage;
    }

    public async System.Threading.Tasks.Task<CGMessage> EndGameAsync(Game game)
    {
      CGMessage returnMessage = new CGMessage();
      try
      {
        await _gameEngine.EndGameAsync(game, _gameHub);
        returnMessage.Status = true;
      }
      catch (Exception ex)
      {
        _logger.Log(LogLevel.Error, $"Method:EndGameAsync; Error: {ex.Message}", returnMessage);
      }
      return returnMessage;
    }

    public CGMessage IsPlayerInGame(Player p)
    {
      CGMessage returnMessage = new CGMessage();
      try
      {
        foreach (Game gameEngineGame in from gameEngineGame in _gameEngine.Games
                  let currentGamePlayer = gameEngineGame.Players.FirstOrDefault(pl => pl.Id.Equals(p.Id))
                  where currentGamePlayer != null && !gameEngineGame.Finished
                  select gameEngineGame)
        {
          returnMessage.ReturnData.Add(gameEngineGame);
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
