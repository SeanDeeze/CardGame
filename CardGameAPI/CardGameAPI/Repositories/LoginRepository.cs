using System;
using System.Collections.Generic;
using System.Linq;
using CardGame.Controllers;
using CardGame.Models;
using CardGame.Models.dto;
using Microsoft.Extensions.Logging;

namespace CardGame.Repositories
{
    public class LoginRepository(EFContext context, ICoordinator gameEngine, ILogger<LoginController> logger)
    {
        private const string ClassName = "LoginRepository";
        private string _methodName = string.Empty;

        private readonly EFContext _context = context;
        private readonly ICoordinator _gameEngine = gameEngine;
        private readonly ILogger<LoginController> _logger = logger;

        public CGMessage Login(User user)
        {
            _methodName = $"{ClassName}.Login";
            CGMessage returnMessage = new();
            try
            {
                User currentPlayer = _gameEngine.GetPlayers().FirstOrDefault(p => p.UserName.ToLower().Equals(user.UserName.Trim().ToLower()));
                // Player is active, just return that info
                if (currentPlayer != null)
                {
                    currentPlayer.LastActivity = DateTimeOffset.Now;
                    _context.SaveChanges();
                    returnMessage.ReturnData.Add(currentPlayer);
                }
                else // Initial Login, Update login activity and add to GameEngine
                {
                    // Does user exist in DB
                    User dbPlayer = _context.Users.FirstOrDefault(p => p.UserName.ToLower().Equals(user.UserName.Trim().ToLower()));
                    if (dbPlayer == null)
                    {
                        _context.Users.Add(user);
                        _context.SaveChanges();
                        dbPlayer = _context.Users.FirstOrDefault(p => p.UserName.ToLower().Equals(user.UserName.Trim().ToLower()));

                    }
                    dbPlayer.LastActivity = DateTimeOffset.Now;
                    _gameEngine.GetPlayers().Add(dbPlayer);
                    returnMessage.ReturnData.Add(dbPlayer);

                }
                returnMessage.Status = true;
            }
            catch (Exception ex)
            {
                returnMessage.Message = $"Error Logging in. See Logs for more details. Error: {ex.Message}";
                _logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}", returnMessage);
            }
            return returnMessage;
        }

        public CGMessage Logout(User player)
        {
            _methodName = $"{ClassName}.Logout";
            CGMessage returnMessage = new();
            try
            {
                if (player == null)
                {
                    return returnMessage;
                }

                User currentPlayer = _gameEngine.GetPlayers().FirstOrDefault(p => p.ID.Equals(player.ID));
                if (currentPlayer != null)
                {
                    _gameEngine.GetPlayers().Remove(currentPlayer);
                    List<User> players = _gameEngine.GetPlayers();
                    returnMessage.ReturnData.Add(players);
                    foreach (Game g in _gameEngine.GetGames())
                    {
                        g.Engine.GamePlayers.RemoveAll(p => p.Player.ID.Equals(player.ID));
                    }
                    returnMessage.Status = true;
                }
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}", returnMessage);
            }
            return returnMessage;
        }
    }
}
