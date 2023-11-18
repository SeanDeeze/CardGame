using CardGame.Models;
using CardGame.Models.dto;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame.Repositories
{
    public class GameEngine(Logger logger, IQueryable<Card> cards)
    {
        private readonly string ClassName = "GameEngine";
        private string _methodName = string.Empty;
        private readonly Logger _logger = logger;
        private static readonly Random Rng = new();
        public List<GamePlayer> GamePlayers { get; set; } = [];
        public Guid CurrentGamePlayerID { get; set; } = new();
        public List<Card> Cards { get; set; } = [.. cards];
        public bool Started { get; set; } = false;
        public bool Finished { get; set; } = false;

        public GamePlayer GetGamePlayerById(Guid ID)
        {
            return GamePlayers.FirstOrDefault(gp => gp.Player.ID == ID);
        }

        public bool AddGamePlayer(GamePlayer player)
        {
            _methodName = $"{ClassName}.AddGamePlayer";
            bool returnBool = false;
            try
            {
                GamePlayers.Add(player);
                returnBool = true;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}");
            }

            return returnBool;
        }

        public bool RemoveGamePlayer(Guid gamePlayerID)
        {
            _methodName = $"{ClassName}.RemoveGamePlayer";
            bool returnBool = false;
            try
            {
                GamePlayers.RemoveAll(pl => pl.Player.ID.Equals(gamePlayerID));
                returnBool = true;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}");
            }

            return returnBool;
        }

        public int GetGamePlayerCount()
        {
            return GamePlayers.Count;
        }

        public void SetCurrentGamePlayer(Guid ID)
        {
            CurrentGamePlayerID = ID;
        }

        public bool StartGame()
        {
            _methodName = $"{ClassName}.StartGame";
            bool returnStatus = false;
            try
            {
                Cards = ShuffleCards(Cards);
                GamePlayers = ShufflePlayers(GamePlayers);
                Started = true;

                for (int i = 0; i < Cards.Count; i++)
                {
                    Card card = Cards[i];
                    card.PileNumber = i % 6;
                    _logger.Log(LogLevel.Debug, $"{_methodName}; Adding Card: {card.Name} to CardPile {card.PileNumber}");
                }

                foreach (GamePlayer gameGamePlayer in GamePlayers)
                {
                    gameGamePlayer.Dice = RollDice();
                }

                CurrentGamePlayerID = GamePlayers.First().Player.ID;

                returnStatus = true;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}");
            }

            return returnStatus;
        }

        public bool EndGame()
        {
            bool returnStatus = false;
            try
            {
                Finished = true;
                returnStatus = true;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}");
            }

            return returnStatus;
        }

        public List<Card> ShuffleCards(List<Card> cards)
        {
            _methodName = $"{ClassName}.ShuffleCards";
            List<Card> returnList = [];
            try
            {
                returnList = [.. cards.OrderBy(a => Rng.Next())];
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"{_methodName}; Error: {ex.Message}");
            }
            return returnList;
        }

        public List<GamePlayer> ShufflePlayers(List<GamePlayer> players)
        {
            _methodName = $"{ClassName}.ShufflePlayers";
            List<GamePlayer> returnList = [];
            try
            {
                returnList = [.. players.OrderBy(a => Rng.Next())];
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}");
            }
            return returnList;
        }

        public List<Dice> RollDice()
        {
            _methodName = $"{ClassName}.RollDice";
            List<Dice> returnList = [];
            try
            {
                Random rnd = new();
                returnList =
                [
                    new Dice(rnd.Next(1, 7)),
                    new Dice(rnd.Next(1, 7)),
                    new Dice(rnd.Next(1, 7)),
                    new Dice(rnd.Next(1, 7)),
                    new Dice(rnd.Next(1, 7)),
                    new Dice(rnd.Next(1, 7))
                ];
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}");
            }
            return returnList;
        }

        public GameState GetGameState()
        {
            _methodName = $"{ClassName}.GetGameState";
            GameState gameState = null;
            try
            {
                gameState = new()
                {
                    Started = Started,
                    GamePlayers = GamePlayers,
                    Cards = Cards,
                    CurrentGamePlayerID = CurrentGamePlayerID
                };

            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, $"{_methodName}; Error: {ex.Message}", gameState);
            }
            return gameState;
        }
    }
}
