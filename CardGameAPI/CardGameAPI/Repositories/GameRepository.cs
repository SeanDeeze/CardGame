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
        private readonly ILogger<GameController> _logger;
        private readonly EFContext _context;
        private readonly IGameEngine _gameEngine;
        private const string ClassName = "GameRepository";
        private string _methodName = string.Empty;
        public GameRepository(EFContext context, IGameEngine gameEngine, ILogger<GameController> logger)
        {
            _context = context;
            _gameEngine = gameEngine;
            _logger = logger;
        }

        public CGMessage GetGames()
        {
            _methodName = $"{ClassName}.GetGames";
            CGMessage returnMessage = new CGMessage();
            try
            {
                List<Game> games = _gameEngine.GetGames().ToList();
                returnMessage.ReturnData.Add(games);
                returnMessage.Status = true;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}", returnMessage);
            }
            return returnMessage;
        }

        public CGMessage GetGameState(int gameId)
        {
            _methodName = $"{ClassName}.GetGameState";
            CGMessage returnMessage = new CGMessage();
            try
            {
                Game game = _gameEngine.GetGames().FirstOrDefault(g => g.Id == gameId);
                returnMessage.ReturnData.Add(game);
                returnMessage.Status = game != null;
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
            CGMessage returnMessage = new CGMessage();
            try
            {
                inputGame.Cards = _gameEngine.GetCards();
                _context.Games.Add(inputGame);
                _context.SaveChanges();
                _gameEngine.AddGame(inputGame);
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
            CGMessage returnMessage = new CGMessage();
            try
            {
                Game currentGame = _gameEngine.GetGames().FirstOrDefault(g => g.Id.Equals(inputGame.Id));
                if (currentGame != null)
                {
                    _context.Games.Remove(currentGame);
                }
                _context.SaveChanges();
                _gameEngine.RemoveGame(_gameEngine.GetGames().Find(g => g.Id.Equals(inputGame.Id)));
                return GetGames();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex,$"{_methodName}; Error: {ex.Message}", returnMessage);
            }
            return returnMessage;
        }

        public CGMessage JoinGame(PlayerGame playerGame)
        {
            CGMessage returnMessage = new CGMessage();
            try
            {
                Game game = _gameEngine.GetGames().First(ge => ge.Id.Equals(playerGame.Game.Id));
                Player p = _gameEngine.GetPlayers().First(pl => pl.Id.Equals(playerGame.Player.Id));
                _gameEngine.AddPlayer(p, game);
                returnMessage.Status = true;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, $"Method:JoinGame; Error: {ex.Message}");
            }
            return returnMessage;
        }

        public CGMessage LeaveGame(PlayerGame playerGame)
        {
            _methodName = $"{ClassName}.LeaveGame";
            CGMessage returnMessage = new CGMessage();
            try
            {
                Game game = _gameEngine.GetGames().First(ge => ge.Id.Equals(playerGame.Game.Id));
                Player p = _gameEngine.GetPlayers().First(pl => pl.Id.Equals(playerGame.Player.Id));
                game.Players.RemoveAll(pl => pl.Id.Equals(playerGame.Player.Id));
                return GetGames();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}");
            }
            return returnMessage;
        }

        public CGMessage StartGame(Game game)
        {
            _methodName = $"{ClassName}.StartGame";
            CGMessage returnMessage = new CGMessage();
            try
            {
                _gameEngine.StartGame(game.Id);
                returnMessage.Status = true;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex,$"{_methodName}; Error: {ex.Message}");
            }
            return returnMessage;
        }

        public CGMessage EndGame(Game game)
        {
            _methodName = $"{ClassName}.EndGame";
            CGMessage returnMessage = new CGMessage();
            try
            {
                _gameEngine.EndGame(game);
                returnMessage.Status = true;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}");
            }
            return returnMessage;
        }
    }
}
