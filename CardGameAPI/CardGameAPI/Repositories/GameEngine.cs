using System;
using CardGameAPI.Models;
using System.Collections.Generic;
using System.Linq;
using NLog;
using CardGameAPI.Hubs;

namespace CardGameAPI.Repositories
{
  public interface IGameEngine
  {
    List<Game> GetGames();
    string GetGameNameById(int id);
    List<Player> GetLoggedInUsers();
    List<Player> GetPlayersInGameById(int gameId);
    Game GetGameState(int gameId);
  }

  public class GameEngine : IGameEngine
  {
    public List<Game> _games;
    public List<Player> _players;
    public List<Card> _cards;
    public readonly List<CardRole> _cardRoles;
    public readonly EFContext _context;
    public readonly Logger _logger;
    private readonly GameHub _gameHub;
    private static readonly Random Rng = new Random();

    public GameEngine(EFContext context)
    {
      _context = context;
      _games = _context.Games.ToList();
      _players = new List<Player>();
      _cards = _context.Cards.ToList();
      _cardRoles = _context.CardRoles.ToList();
      _logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
    }

    public List<Game> GetGames()
    {
      try
      {
        return _games;
      }
      catch (Exception ex)
      {
        _logger.Log(LogLevel.Error, $"GameEngine.GetGames; Error: {ex.Message}");
      }
      return new List<Game>();
    }

    public List<Card> GetCards()
    {
      try
      {
        return _cards;
      }
      catch (Exception ex)
      {
        _logger.Log(LogLevel.Error, $"GameEngine.GetCards; Error: {ex.Message}");
      }
      return new List<Card>();
    }

    public Game GetGameState(int gameId)
    {
      try
      {
        return _games.FirstOrDefault(g => g.Id == gameId); ;
      }
      catch (Exception ex)
      {
        _logger.Log(LogLevel.Error, $"GameEngine.GetGameState; Error: {ex.Message}");
      }
      return null;
    }

    public string GetGameNameById(int gameId)
    {
      try
      {
        return _games.First(g => g.Id.Equals(gameId)).Name;
      }
      catch (Exception ex)
      {
        _logger.Log(LogLevel.Error, $"GameEngine.GetGameNameById; Error: {ex.Message}");
      }
      return string.Empty;
    }

    public List<Player> GetPlayersInGameById(int gameId)
    {
      try
      {
        return _games.First(g => g.Id.Equals(gameId)).Players.ToList();
      }
      catch (Exception ex)
      {
        _logger.Log(LogLevel.Error, $"GetPlayersInGameById; Error: {ex.Message}");
      }
      return new List<Player>();
    }

    public List<Player> GetLoggedInUsers()
    {
      try
      {
        return _players.ToList();
      }
      catch (Exception ex)
      {
        _logger.Log(LogLevel.Error, $"GetLoggedInUsers; Error: {ex.Message}");
      }
      return new List<Player>();
    }

    public async System.Threading.Tasks.Task StartGameAsync(int gameId)
    {
      try
      {
        Game game = _games.First(g => g.Id.Equals(gameId));
        if (game != null)
        {
          game.Active = true;
          game.Cards = ShuffleCards(game.Cards);
          game.Players = ShufflePlayers(game.Players);
          game.Active = true;
          _context.Games.Update(game);
          _context.SaveChanges();

          foreach (Player p in game.Players)
          {
            p.CurrentGame = game;
            p.LastActivity = DateTime.Now;
            p.Points = 0;
            p.Gold = 0;
          }
          for (int i = 0; i < 6; i++)
          {
            game.CardPiles.Add(new List<Card>());
          }
          for (int i = 0; i < game.Cards.Count; i++)
          {
            game.CardPiles[i % 6].Add(game.Cards[i]);
          }

          await _gameHub.SendGameState(game.Id);
        }
      }
      catch (Exception ex)
      {
        _logger.Log(LogLevel.Error, $"GameEngine.StartGame; Error: {ex.Message}");
      }
    }

    public async System.Threading.Tasks.Task EndGame(int gameId)
    {
      try
      {
        Game game = _games.First(g => g.Id.Equals(gameId));
        if (game != null)
        {
          game.Active = false;
          game.Finished = true;
          _context.Games.Update(game);
          _context.SaveChanges();

          await _gameHub.SendGameState(game.Id);
        }
      }
      catch (Exception ex)
      {
        _logger.Log(LogLevel.Error, $"GameEngine.StartGame; Error: {ex.Message}");
      }
    }

    public List<Card> ShuffleCards(List<Card> cards)
    {
      try
      {
        return cards.OrderBy(a => Rng.Next()).ToList();
      }
      catch (Exception ex)
      {
        _logger.Log(LogLevel.Error, $"GameEngine.ShuffleCards; Error: {ex.Message}");
      }
      return new List<Card>();
    }

    public List<Player> ShufflePlayers(List<Player> players)
    {
      try
      {
        return players.OrderBy(a => Rng.Next()).ToList();
      }
      catch (Exception ex)
      {
        _logger.Log(LogLevel.Error, $"GameEngine.ShuffleCards; Error: {ex.Message}");
      }
      return new List<Player>();
    }
  }
}
