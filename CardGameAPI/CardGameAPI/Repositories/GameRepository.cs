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
        private readonly IGameEngine _gameEngine;
        public GameRepository(EFContext context, IGameEngine gameEngine, ILogger<GameController> logger)
        {
            _context = context;
            _gameEngine = gameEngine;
            _logger = logger;
        }

        public CGMessage GetGames()
        {
            _methodName = $"{ClassName}.GetGames";
            CGMessage returnMessage = new();
            try
            {
                List<Game> games = _gameEngine.GetGames().ToList();
                foreach (Game game in games)
                {
                    game.Cards = null;
                }
                returnMessage.ReturnData.Add(games);
                returnMessage.Status = true;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}", returnMessage);
            }
            return returnMessage;
        }

        public CGMessage GetGameState(Game game)
        {
            _methodName = $"{ClassName}.GetGameState";
            CGMessage returnMessage = new();
            try
            {
                Game selectedGame = _gameEngine.GetGames().FirstOrDefault(g => g.Id == game.Id);

                if (selectedGame == null)
                {
                    returnMessage.Message = $"Error: No Game found with GameId {game.Id}";
                    _logger.Log(LogLevel.Error, $"{_methodName}; Error: No Game found with GameId {game.Id}", returnMessage);
                    return returnMessage;
                }

                GameState gameState = new()
                {
                    GamePlayers = selectedGame.GamePlayers,
                    CardPiles = selectedGame.CardPiles,
                    CurrentGamePlayer = selectedGame.GamePlayers.First(gp => gp.IsCurrent)
                };

                returnMessage.ReturnData.Add(gameState);
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
            CGMessage returnMessage = new();
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
                Game game = _gameEngine.GetGames().First(ge => ge.Id.Equals(playerGame.Game.Id));
                Player p = _gameEngine.GetPlayers().First(pl => pl.Id.Equals(playerGame.Player.Id));
                _gameEngine.AddPlayer(p, game);
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
                Game game = _gameEngine.GetGames().First(ge => ge.Id.Equals(playerGame.Game.Id));
                Player p = _gameEngine.GetPlayers().First(pl => pl.Id.Equals(playerGame.Player.Id));
                game.GamePlayers.RemoveAll(pl => pl.Player.Id.Equals(playerGame.Player.Id));
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
                _gameEngine.StartGame(game.Id);
                returnMessage.Status = true;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}", returnMessage);
            }
            return returnMessage;
        }

        public CGMessage EndGame(Game game)
        {
            _methodName = $"{ClassName}.EndGame";
            CGMessage returnMessage = new();
            try
            {
                _gameEngine.EndGame(game);
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
