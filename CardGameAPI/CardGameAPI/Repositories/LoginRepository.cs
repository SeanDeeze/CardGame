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
  public class LoginRepository
  {
    private readonly EFContext _context;
    private readonly IGameEngine _gameEngine;
    private readonly ILogger<LoginController> _logger;
    private readonly IHubContext<GameHub> _gameHub;

    public LoginRepository(EFContext context, IGameEngine gameEngine, IHubContext<GameHub> gameHub, ILogger<LoginController> logger)
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
        Player currentPlayer = _gameEngine.GetPlayers().FirstOrDefault(p => p.UserName.ToLower().Equals(player.UserName.Trim().ToLower()));
        if (currentPlayer != null) // Player is active, just return that info
        {
          currentPlayer.LastActivity = DateTime.Now;
          _context.SaveChanges();
          returnMessage.ReturnData.Add(currentPlayer);
          List<Player> players = _gameEngine.GetLoggedInUsers();
          _ = _gameHub.Clients.All.SendAsync("ReceiveLoggedInUsers", players);
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
            _gameEngine.GetPlayers().Add(dbPlayer);
            returnMessage.ReturnData.Add(dbPlayer);
            List<Player> players = _gameEngine.GetLoggedInUsers();
            _ = _gameHub.Clients.All.SendAsync("ReceiveLoggedInUsers", players);
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
        Player currentPlayer = _gameEngine.GetPlayers().FirstOrDefault(p => p.Id.Equals(player.Id));
        if (currentPlayer != null)
        {
          _gameEngine.GetPlayers().Remove(currentPlayer);
          List<Player> players = _gameEngine.GetLoggedInUsers();
          returnMessage.ReturnData.Add(players);
          foreach (Game g in _gameEngine.GetGames())
          {
            g.Players.RemoveAll(p => p.Id.Equals(player.Id));
          }

          _ = _gameHub.Clients.All.SendAsync("ReceiveLoggedInUsers", players);
          returnMessage.Status = true;
        }
      }
      catch (Exception ex)
      {
        _logger.Log(LogLevel.Error, $"Method:Logout; Error: {ex.Message}", returnMessage);
      }
      return returnMessage;
    }
  }
}
