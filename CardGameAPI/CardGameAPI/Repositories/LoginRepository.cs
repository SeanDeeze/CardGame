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
  public class LoginRepository
  {
    private EFContext _context;
    private GameEngine _gameEngine;
    private readonly ILogger<LoginController> _logger;
    private IHubContext<GameHub> _gameHub;

    public LoginRepository(EFContext context, GameEngine gameEngine, IHubContext<GameHub> gameHub, ILogger<LoginController> logger)
    {
      _context = context;
      _gameEngine = gameEngine;
      _logger = logger;
      _gameHub = gameHub;
    }

    public CGMessage Login(Player player)
    {
      CGMessage returnMessage = new CGMessage();
      try
      {
        Player currentPlayer = _gameEngine.Players.FirstOrDefault(p => p.UserName.ToLower().Equals(player.UserName.Trim().ToLower()));
        if (currentPlayer != null) // Player is active, just return that info
        {
          currentPlayer.LastActivity = DateTime.Now;
          _context.SaveChanges();
          returnMessage.ReturnData.Add(currentPlayer);
        }
        else // Initial Login, Update login activity and add to GameEngine
        {
          Player dbPlayer = _context.Players.FirstOrDefault(p => p.UserName.ToLower().Equals(player.UserName.Trim().ToLower())); // Does user exist in DB
          if (dbPlayer == null)
          {
            _context.Players.Add(player);
            _context.SaveChanges();
            dbPlayer = _context.Players.FirstOrDefault(p => p.UserName.ToLower().Equals(player.UserName.Trim().ToLower()));
          }
          if (dbPlayer != null)
          {
            dbPlayer.LastActivity = DateTime.Now;
            _gameEngine.Players.Add(dbPlayer);
            returnMessage.ReturnData.Add(dbPlayer);
            List<Player> players = _gameEngine.Players.ToList();
            _gameHub.Clients.All.SendAsync("ReceiveLoggedInUsers", players);
          }


        }
        returnMessage.Status = true;
      }
      catch (Exception ex)
      {
        _logger.Log(LogLevel.Error, $"Method:Login; Error: {ex.Message}", returnMessage);
      }
      return returnMessage;
    }

    public CGMessage Logout(Player player)
    {
      CGMessage returnMessage = new CGMessage();
      try
      {
        Player currentPlayer = _gameEngine.Players.FirstOrDefault(p => p.Id.Equals(player.Id));
        if (currentPlayer != null)
        {
          _gameEngine.Players.Remove(currentPlayer);
          List<Player> players = _gameEngine.Players.ToList();
          returnMessage.ReturnData.Add(players);
          foreach (var g in _gameEngine.Games)
          {
            g.Players.RemoveAll(p => p.Id.Equals(player.Id));
          }

          _gameHub.Clients.All.SendAsync("ReceiveLoggedInUsers", players);
          returnMessage.Status = true;
        }
      }
      catch (Exception ex)
      {
        _logger.Log(LogLevel.Error, $"Method:Logout; Error: {ex.Message}", returnMessage);
      }
      return returnMessage;
    }

    public CGMessage GetLoggedInPlayers()
    {
      CGMessage returnMessage = new CGMessage();
      try
      {
        var players = _gameEngine.Players.Where(p => p.LastActivity >= DateTime.Now.AddMinutes(-120)); // Select all players active in last minute
        if (players != null)
        {
          returnMessage.ReturnData.Add(players.ToList());
        }
        returnMessage.Status = true;
      }
      catch (Exception ex)
      {
        _logger.Log(LogLevel.Error, $"Method:GetLoggedInPlayers; Error: {ex.Message}", returnMessage);
      }
      return returnMessage;
    }
  }
}
