using CardGame.Controllers;
using CardGame.Models;
using CardGame.Models.dto;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame.Repositories
{
    public class GameRepository
    {
        private const string ClassName = "GameRepository";
        private string _methodName = string.Empty;

        private readonly ILogger<GameController> _logger;
        private readonly EFContext _context;
        private readonly ICoordinator _coordinator;
        public GameRepository(EFContext context, ICoordinator gameEngine, ILogger<GameController> logger)
        {
            _context = context;
            _coordinator = gameEngine;
            _logger = logger;
        }

        public CGMessage GetGames()
        {
            _methodName = $"{ClassName}.GetGames";
            CGMessage returnMessage = new();
            try
            {
                List<Game> games = _coordinator.GetGames().ToList();
                returnMessage.ReturnData.Add(games);
                returnMessage.Status = true;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}", returnMessage);
            }
            return returnMessage;
        }

        public CGMessage SaveGame(Game inputGame)
        {
            _methodName = $"{ClassName}.SaveGame";
            CGMessage returnMessage = new();
            try
            {
                _context.Games.Add(inputGame);
                _context.SaveChanges();
                _coordinator.AddGame(inputGame);
                return GetGames();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}", returnMessage);
            }
            return returnMessage;
        }

        public CGMessage DeleteGame(Game inputGame)
        {
            _methodName = $"{ClassName}.DeleteGame";
            CGMessage returnMessage = new();
            try
            {
                Game currentGame = _coordinator.GetGame(inputGame.ID);
                if (currentGame != null)
                {
                    _context.Games.Remove(currentGame);
                }
                _context.SaveChanges();
                _coordinator.RemoveGame(_coordinator.GetGame(inputGame.ID));
                return GetGames();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}", returnMessage);
            }
            return returnMessage;
        }

        public CGMessage JoinGame(PlayerGame playerGame)
        {
            _methodName = $"{ClassName}.JoinGame";
            CGMessage returnMessage = new();
            try
            {
                Game game = _coordinator.GetGame(playerGame.Game.ID);
                User p = _coordinator.GetPlayers().First(pl => pl.ID.Equals(playerGame.Player.ID));
                _coordinator.AddPlayer(p, game);
                returnMessage.Status = true;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}", returnMessage);
            }
            return returnMessage;
        }

        public CGMessage LeaveGame(PlayerGame playerGame)
        {
            _methodName = $"{ClassName}.LeaveGame";
            CGMessage returnMessage = new();
            try
            {
                return GetGames();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}", returnMessage);
            }
            return returnMessage;
        }

        public CGMessage StartGame(Game game)
        {
            _methodName = $"{ClassName}.StartGame";
            CGMessage returnMessage = new();
            try
            {
                _coordinator.StartGame(game.ID);
                returnMessage.Status = true;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}", returnMessage);
            }
            return returnMessage;
        }

        public CGMessage EndGame(Guid gameId)
        {
            _methodName = $"{ClassName}.EndGame";
            CGMessage returnMessage = new();
            try
            {
                _coordinator.EndGame(gameId);
                returnMessage.Status = true;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}", returnMessage);
            }
            return returnMessage;
        }

        public CGMessage GetGameState(Guid gameID)
        {
            _methodName = $"{ClassName}.GetGameState";
            CGMessage returnMessage = new();
            try
            {
                GameState gameState = _coordinator.GetGame(gameID).Engine.GetGameState();
                returnMessage.ReturnData.Add(gameState);
                returnMessage.Status = true;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}", returnMessage);
            }
            return returnMessage;
        }
    }
}
