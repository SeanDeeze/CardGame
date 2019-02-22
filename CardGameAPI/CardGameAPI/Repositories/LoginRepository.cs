using CardGameAPI.Controllers;
using CardGameAPI.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace CardGameAPI.Repositories
{
  public class LoginRepository
  {
    private EFContext _context;
    private GameEngine _gameEngine;
    private readonly ILogger<LoginController> _logger;

    public LoginRepository(EFContext context, GameEngine gameEngine, ILogger<LoginController> logger)
    {
      _context = context;
      _gameEngine = gameEngine;
      _logger = logger;
    }

    public CGMessage Login(Player player)
    {
      CGMessage returnMessage = new CGMessage();
      try
      {
        Player currentPlayer = _gameEngine._players.FirstOrDefault(p => p.UserName.ToLower().Equals(player.UserName.Trim().ToLower()));
        if (currentPlayer != null)
        {
          currentPlayer.LastActivity = DateTime.Now;
          returnMessage.ReturnData.Add(currentPlayer);
        }
        else
        {
          player.LastActivity = DateTime.Now;
          _context.Players.Add(player);
          _context.SaveChanges();
          currentPlayer = _context.Players.FirstOrDefault(p => p.UserName.ToLower().Equals(player.UserName.Trim().ToLower()));
          _gameEngine._players.Add(currentPlayer);
          returnMessage.ReturnData.Add(currentPlayer);
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
        Player currentPlayer = _gameEngine._players.FirstOrDefault(p => p.Id.Equals(player.Id));
        if (currentPlayer != null)
        {
          currentPlayer.LastActivity = null;
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
        var players = _gameEngine._players.Where(p => p.LastActivity >= DateTime.Now.AddMinutes(-1)); // Select all players active in last minute
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

    public CGMessage KeepAlive(Player player)
    {
      CGMessage returnMessage = new CGMessage();
      try
      {
        Player currentPlayer = _gameEngine._players.FirstOrDefault(p => p.UserName.ToLower().Equals(player.UserName.Trim().ToLower()));
        if (currentPlayer != null)
        {
          currentPlayer.LastActivity = DateTime.Now;
          returnMessage.ReturnData.Add(currentPlayer);
          returnMessage.Status = true;
        }
      }
      catch (Exception ex)
      {
        _logger.Log(LogLevel.Error, $"Method:KeepAlive; Error: {ex.Message}", returnMessage);
      }
      return returnMessage;
    }
  }
}
