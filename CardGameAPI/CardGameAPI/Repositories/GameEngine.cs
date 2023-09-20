using CardGame.Models;
using NLog;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame.Repositories
{
    public interface IGameEngine
    {
        List<User> GetPlayers();
        List<Game> GetGames();
        List<Card> GetCards();
        bool AddPlayer(User p, Game g);
        bool AddGame(Game game);
        bool RemoveGame(Game game);
        bool StartGame(int gameId);
        bool EndGame(Game game);
    }

    public class GameEngine : IGameEngine
    {
        private const string ClassName = "GameEngine";
        private string _methodName = string.Empty;

        public List<Game> Games;
        public List<User> Players;
        public List<Card> Cards;
        public readonly List<CardRole> CardRoles;
        public readonly EFContext Context;
        public readonly Logger Logger;
        private static readonly Random Rng = new();

        public GameEngine(EFContext context)
        {
            Context = context;
            Games = Context.Games.ToList();
            Cards = Context.Cards.ToList();
            CardRoles = Context.CardRoles.ToList();
            Players = new List<User>();
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
                returnPlayers = Players;
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

        public bool AddPlayer(User player, Game game)
        {
            _methodName = $"{ClassName}.AddPlayer";
            bool methodStatus = false;
            try
            {
                // Search For Player already in Game, if not found then add
                if (game.GamePlayers.Find(gp => gp.Player.Id.Equals(player.Id)) == null)
                {
                    GamePlayer gamePlayer = new()
                    {
                        Player = player,
                        Order = game.GamePlayers.Count
                    };

                    if (game.GamePlayers.Count == 0)
                    {
                        gamePlayer.Leader = true;
                        game.CurrentGamePlayer = player.Id;

                    }
                    game.GamePlayers.Add(gamePlayer);
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

        public List<Card> GetCards()
        {
            _methodName = $"{ClassName}.GetCards";
            List<Card> returnList = new();
            try
            {
                returnList = Cards;
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}");
            }
            return returnList;
        }

        public bool StartGame(int gameId)
        {
            _methodName = $"{ClassName}.StartGame";
            bool returnStatus = false;
            try
            {
                Game game = Games.First(g => g.Id.Equals(gameId));
                game.Cards = GetCards();
                game.Cards = ShuffleCards(game.Cards);
                game.GamePlayers = ShufflePlayers(game.GamePlayers);
                game.Active = true;

                Context.Games.Update(game);
                Context.SaveChanges();

                for (int i = 0; i < game.Cards.Count; i++)
                {
                    Card card = game.Cards[i];
                    card.PileNumber = i % 6;
                    Logger.Log(LogLevel.Debug, $"{_methodName}; Adding Card: {card.Name} to CardPile {card.PileNumber}");
                }

                foreach (GamePlayer gameGamePlayer in game.GamePlayers)
                {
                    gameGamePlayer.Dice = RollDice();
                }

                game.CurrentGamePlayer = game.GamePlayers.First().Player.Id;

                returnStatus = true;
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}");
            }

            return returnStatus;
        }

        public bool EndGame(Game game)
        {
            bool returnStatus = false;
            try
            {
                if (game == null)
                {
                    Logger.Log(LogLevel.Error, $"{_methodName}; Game Object sent to Method is null");
                    return false;
                }

                Game currentGame = Context.Games.FirstOrDefault(g => g.Id == game.Id);
                if (currentGame == null)
                {
                    Logger.Log(LogLevel.Error, $"{_methodName}; Could not find Game Record with Id: {game.Id}");
                    return false;
                }

                game.Active = false;
                game.Finished = true;
                Context.Entry(currentGame).CurrentValues.SetValues(game);
                Context.SaveChanges();

                Games.First(g => g.Id.Equals(game.Id)).Active = false;
                Games.First(g => g.Id.Equals(game.Id)).Finished = true;

                returnStatus = true;

            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}");
                returnStatus = false;
            }

            return returnStatus;
        }

        public List<Card> ShuffleCards(List<Card> cards)
        {
            _methodName = $"{ClassName}.ShuffleCards";
            List<Card> returnList = new();
            try
            {
                returnList = cards.OrderBy(a => Rng.Next()).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"{_methodName}; Error: {ex.Message}");
            }
            return returnList;
        }

        public List<GamePlayer> ShufflePlayers(List<GamePlayer> players)
        {
            _methodName = $"{ClassName}.ShufflePlayers";
            List<GamePlayer> returnList = new();
            try
            {
                returnList = players.OrderBy(a => Rng.Next()).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}");
            }
            return returnList;
        }

        public List<int> RollDice()
        {
            _methodName = $"{ClassName}.RollDice";
            List<int> returnList = new();
            try
            {
                Random rnd = new();

                returnList = new List<int>
                {
                    rnd.Next(1, 7),
                    rnd.Next(1, 7),
                    rnd.Next(1, 7),
                    rnd.Next(1, 7),
                    rnd.Next(1, 7),
                    rnd.Next(1, 7)
                };
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}");
            }
            return returnList;
        }
    }
}
