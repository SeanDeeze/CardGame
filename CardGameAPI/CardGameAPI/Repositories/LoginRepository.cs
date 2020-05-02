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
    private readonly GameEngine _gameEngine;
    private readonly ILogger<LoginController> _logger;
    private readonly IHubContext<GameHub> _gameHub;

    public LoginRepository(EFContext context, GameEngine gameEngine, IHubContext<GameHub> gameHub, ILogger<LoginController> logger)
    {
      _context = context;
      _gameEngine = gameEngine;
      _logger = logger;
      _gameHub = gameHub;
    }

    public async Task<CGMessage> Login(Player player)
    {
      CGMessage returnMessage = new CGMessage();
      try
      {
        Player currentPlayer = _gameEngine.Players.FirstOrDefault(p => p.UserName.ToLower().Equals(player.UserName.Trim().ToLower()));
        if (currentPlayer != null) // Player is active, just return that info
        {
          currentPlayer.LastActivity = DateTime.Now;
          await _context.SaveChangesAsync();
          returnMessage.ReturnData.Add(currentPlayer);
          List<Player> players = _gameEngine.Players.ToList();
          _ = _gameHub.Clients.All.SendAsync("ReceiveLoggedInUsers", players);
        }
        else // Initial Login, Update login activity and add to GameEngine
        {
          Player dbPlayer = _context.Players.FirstOrDefault(p => p.UserName.ToLower().Equals(player.UserName.Trim().ToLower())); // Does user exist in DB
          if (dbPlayer == null)
          {
            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
            //Add to gameEngine
            dbPlayer = _context.Players.FirstOrDefault(p => p.UserName.ToLower().Equals(player.UserName.Trim().ToLower()));
          }

          if (dbPlayer != null)
          {
            dbPlayer.LastActivity = DateTime.Now;
            _gameEngine.Players.Add(dbPlayer);
            returnMessage.ReturnData.Add(dbPlayer);
            List<Player> players = _gameEngine.Players.ToList();
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
        var players = _gameEngine.Players.Where(p => p.LastActivity >= DateTime.Now.AddSeconds(-60)); // Select all players active in last minute
        returnMessage.ReturnData.Add(players.ToList());
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
