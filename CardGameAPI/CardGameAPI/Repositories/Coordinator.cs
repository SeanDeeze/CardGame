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
        bool AddPlayer(User p, Game g);
        bool AddGame(Game game);
        bool RemoveGame(Game game);
        bool StartGame(int gameId);
        bool EndGame(int gameId);
    }

    public class Coordinator : ICoordinator
    {
        private const string ClassName = "GameEngine";
        private string _methodName = string.Empty;

        public List<Game> Games;
        public List<User> Users;
        public List<Card> Cards;
        public readonly List<CardRole> CardRoles;
        public readonly EFContext Context;
        public readonly Logger Logger;

        public Coordinator(EFContext context)
        {
            Context = context;
            Games = Context.Games.ToList();
            Cards = Context.Cards.ToList();
            CardRoles = Context.CardRoles.ToList();
            Users = new List<User>();
            Logger = LogManager.Setup()
                    .LoadConfigurationFromAppSettings(basePath: AppContext.BaseDirectory)
                    .GetCurrentClassLogger();
        }
        public List<User> GetPlayers()
        {
            _methodName = $"{ClassName}.GetPlayers";
            List<User> returnPlayers = new();
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
                return Games.ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}");
            }

            return new List<Game>();
        }

        public bool AddGame(Game game)
        {
            _methodName = $"{ClassName}.AddGame";
            bool methodStatus = false;
            try
            {
                Games.Add(game);
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

        public bool StartGame(int gameId)
        {
            _methodName = $"{ClassName}.RemoveGame";
            bool methodStatus = false;
            try
            {
                Games.First(game => game.Id == gameId).Engine.StartGame();
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}");
            }

            return methodStatus;
        }

        public bool EndGame(int gameId)
        {
            _methodName = $"{ClassName}.RemoveGame";
            bool methodStatus = false;
            try
            {
                Games.First(game => game.Id == gameId).Engine.EndGame();
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
                if (game.Engine.GetGamePlayerById(player.Id) == null)
                {
                    GamePlayer gamePlayer = new()
                    {
                        Player = player,
                        Order = game.Engine.GetGamePlayerCount()
                    };

                    if (game.Engine.GetGamePlayerCount() == 0)
                    {
                        gamePlayer.Leader = true;
                        game.Engine.SetCurrentGamePlayer(player.Id);

                    }
                    game.Engine.AddGamePlayer(gamePlayer);
                }

                methodStatus = true;
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}");
                methodStatus = false;
            }

            return methodStatus;
        }
    }
}
