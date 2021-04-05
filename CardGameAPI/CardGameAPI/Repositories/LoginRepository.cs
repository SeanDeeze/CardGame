using CardGameAPI.Controllers;
using CardGameAPI.Models;
using CardGameAPI.Models.Dto;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGameAPI.Repositories
{
    public class LoginRepository
    {
        private readonly EFContext _context;
        private readonly IGameEngine _gameEngine;
        private readonly ILogger<LoginController> _logger;

        public LoginRepository(EFContext context, IGameEngine gameEngine, ILogger<LoginController> logger)
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
                Player currentPlayer = _gameEngine.GetPlayers().FirstOrDefault(p => p.UserName.ToLower().Equals(player.UserName.Trim().ToLower()));
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
                        _gameEngine.GetPlayers().Add(dbPlayer);
                        returnMessage.ReturnData.Add(dbPlayer);
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
                if (player == null)
                {
                    return returnMessage;
                }

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
