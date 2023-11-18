using CardGame.Models;
using NLog;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame.Repositories
{
    public interface ICoordinator
    {
        List<User> GetPlayers();
        List<Game> GetGames();
        Game GetGame(Guid GameID);
        bool AddPlayer(User p, Game g);
        bool AddGame(Game game);
        bool RemoveGame(Game game);
        bool StartGame(Guid gameID);
        bool EndGame(Guid gameID);
    }

    public class Coordinator : ICoordinator
    {
        private const string ClassName = "GameEngine";
        private string _methodName = string.Empty;

        public List<Game> Games;
        public List<User> Users;
        public IQueryable<Card> Cards;
        public readonly List<CardRole> CardRoles;
        private readonly EFContext _context;
        public readonly Logger Logger;

        public Coordinator(EFContext context)
        {
            _context = context;
            Games = [];
            Users = [];
            Cards = _context.Cards;
            CardRoles = [.. _context.CardRoles];
            Logger = LogManager.Setup()
                    .LoadConfigurationFromAppSettings(basePath: AppContext.BaseDirectory)
                    .GetCurrentClassLogger();
        }
        public List<User> GetPlayers()
        {
            _methodName = $"{ClassName}.GetPlayers";
            List<User> returnPlayers = [];
            try
            {
                returnPlayers = Users;
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}");
            }

            return returnPlayers;
        }

        public List<Game> GetGames()
        {
            _methodName = $"{ClassName}.GetGames";
            try
            {
                return [.. Games];
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}");
            }

            return [];
        }
        
        public Game GetGame(Guid gameID)
        {
            _methodName = $"{ClassName}.GetGames";
            Game returnGame = null;
            try
            {
                returnGame = Games.FirstOrDefault(game => game.ID.Equals(gameID));
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}");
            }

            return returnGame;
        }

        public bool AddGame(Game game)
        {
            _methodName = $"{ClassName}.AddGame";
            bool methodStatus = false;
            try
            {
                Games.Add(new Game(game, Logger, Cards));
                methodStatus = true;
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}");
            }

            return methodStatus;
        }

        public bool RemoveGame(Game game)
        {
            _methodName = $"{ClassName}.RemoveGame";
            bool methodStatus = false;
            try
            {
                Games.Remove(game);
                methodStatus = true;
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}");
            }

            return methodStatus;
        }

        public bool StartGame(Guid gameID)
        {
            _methodName = $"{ClassName}.RemoveGame";
            bool methodStatus = false;
            try
            {
                GetGame(gameID).Engine.StartGame();
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}");
            }

            return methodStatus;
        }

        public bool EndGame(Guid gameID)
        {
            _methodName = $"{ClassName}.RemoveGame";
            bool methodStatus = false;
            try
            {
                GetGame(gameID).Engine.EndGame();
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}");
            }

            return methodStatus; 
        }

        public bool AddPlayer(User player, Game game)
        {
            _methodName = $"{ClassName}.AddPlayer";
            bool methodStatus = false;
            try
            {
                // Search For Player already in Game, if not found then add
                if (game.Engine.GetGamePlayerById(player.ID) == null)
                {
                    GamePlayer gamePlayer = new()
                    {
                        Player = player,
                        Order = game.Engine.GetGamePlayerCount()
                    };

                    if (game.Engine.GetGamePlayerCount() == 0)
                    {
                        gamePlayer.Leader = true;
                        game.Engine.SetCurrentGamePlayer(player.ID);

                    }
                    game.Engine.AddGamePlayer(gamePlayer);
                }

                methodStatus = true;
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}");
            }

            return methodStatus;
        }
    }
}
