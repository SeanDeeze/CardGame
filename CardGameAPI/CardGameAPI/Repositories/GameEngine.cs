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
        List<Player> GetLoggedInUsers();
    }

    public class GameEngine : IGameEngine
    {
        public List<Game> Games;
        public List<Player> Players;
        public List<Card> Cards;
        public readonly List<CardRole> CardRoles;
        public readonly EFContext Context;
        public readonly Logger Logger;
        private static readonly Random Rng = new Random();
        private const string ClassName = "GameEngine";
        private string _methodName = string.Empty;

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
            List<Player> returnPlayers = new List<Player>();
            try
            {
                returnPlayers = Players;
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"{_methodName}; Error: {ex.Message}");
            }

            return returnPlayers;
        }

        public List<Game> GetGames()
        {
            _methodName = $"{ClassName}. GetGames";
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
                Logger.Log(LogLevel.Error, $"{_methodName}; Error: {ex.Message}");
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
                Game updateGame = game;
                if (game.Players.Find(pl => pl.Id.Equals(p.Id)) == null)
                {
                    game.Players.Add(p);
                }

                Games[Games.IndexOf(updateGame)] = game;

                //Remove player from any other possible games
                foreach (Game forGame in GetGames().Where(forGame => forGame.Id != g.Id))
                {
                    Player curPlayer = forGame.Players.FirstOrDefault(curP => curP.Id == p.Id);
                    if (curPlayer != null)
                    {
                        forGame.Players.Remove(curPlayer);
                    }
                    Games[Games.IndexOf(forGame)] = forGame;
                }
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
            List<Card> returnList = new List<Card>();
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
        public List<Player> GetLoggedInUsers()
        {
            _methodName = $"{ClassName}.GetLoggedInUsers";
            List<Player> returnList = new List<Player>();
            try
            {
                returnList = Players.ToList();
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
                Game updateGame = game;

                game.Active = true;
                game.Cards = ShuffleCards(game.Cards);
                game.Players = ShufflePlayers(game.Players);
                game.Active = true;
                Context.Games.Update(game);
                Context.SaveChanges();

                for (int i = 0; i < game.Players.Count; i++)
                {
                    Player p = game.Players[i];
                    if (i == 0)
                    {
                        p.IsSelectedUser = true;
                    }
                    p.LastActivity = DateTimeOffset.Now;
                    p.Order = i;
                }

                for (int i = 0; i < game.Cards.Count; i++)
                {
                    game.Cards[i].CardPile = i % 6;
                }

                Games[Games.IndexOf(updateGame)] = game;
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
                }
                else
                {
                    Game currentGame = Context.Games.FirstOrDefault(g => g.Id == game.Id);
                    if (currentGame == null)
                    {
                        Logger.Log(LogLevel.Error, $"{_methodName}; Could not find Game Record with Id: {game.Id}");
                    }
                    else
                    {
                        game.Active = false;
                        game.Finished = true;
                        Context.Entry(currentGame).CurrentValues.SetValues(game);
                        Context.SaveChanges();

                        Games.First(g => g.Id.Equals(game.Id)).Active = false;
                        Games.First(g => g.Id.Equals(game.Id)).Finished = true;

                        returnStatus = true;
                    }
                }
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
            List<Card> returnList = new List<Card>();
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

        public List<Player> ShufflePlayers(List<Player> players)
        {
            _methodName = $"{ClassName}.ShufflePlayers";
            List<Player> returnList = new List<Player>();
            try
            {
                returnList = players.OrderBy(a => Rng.Next()).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"{_methodName}; Error: {ex.Message}");
            }
            return returnList;
        }
    }
}
