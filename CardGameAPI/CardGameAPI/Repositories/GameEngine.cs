using CardGame.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame.Repositories
{
    public interface IGameEngine
    {
        List<Player> GetPlayers();
        List<Game> GetGames();
        List<Card> GetCards();
        bool AddPlayer(Player p, Game g);
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
        public List<Player> Players;
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
            Players = new List<Player>();
            Logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetLogger("allfile");
        }
        public List<Player> GetPlayers()
        {
            _methodName = $"{ClassName}.GetPlayers";
            List<Player> returnPlayers = new();
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

        public bool AddPlayer(Player p, Game g)
        {
            _methodName = $"{ClassName}.AddPlayer";
            bool methodStatus = false;
            try
            {
                Game game = Games.FirstOrDefault(sGame => sGame.Id == g.Id);
                if (game == null)
                {
                    Logger.Log(LogLevel.Error, $"{_methodName}; Error: Game Not found with GameId: ({g.Id})");
                    return methodStatus;
                }

                // Search For Player already in Game, if not found then add
                if (game.GamePlayers.Find(gp => gp.Player.Id.Equals(p.Id)) == null)
                {
                    GamePlayer gamePlayer = new()
                    {
                        Player = p,
                        Order = game.GamePlayers.Count
                    };

                    game.GamePlayers.Add(gamePlayer);
                }

                Games[Games.IndexOf(game)] = game;
                methodStatus = true;
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}");
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

                game.Active = true;
                game.Cards = ShuffleCards(game.Cards);
                game.GamePlayers = ShufflePlayers(game.GamePlayers);
                game.Active = true;
                Context.Games.Update(game);
                Context.SaveChanges();

                for (int i = 0; i < game.Cards.Count; i++)
                {
                    Card card = game.Cards[i];
                    int pileNumber = i % 6;
                    game.CardPiles[pileNumber].Add(card);
                    Logger.Log(LogLevel.Debug, $"{_methodName}; Adding Card: {card.Name} to CardPile {pileNumber}");
                }

                game.GamePlayers.First().IsCurrent = true;

                Games[Games.IndexOf(game)] = game;
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
                    return returnStatus;
                }

                Game currentGame = Context.Games.FirstOrDefault(g => g.Id == game.Id);
                if (currentGame == null)
                {
                    Logger.Log(LogLevel.Error, $"{_methodName}; Could not find Game Record with Id: {game.Id}");
                    return returnStatus;
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
                return false;
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
    }
}
