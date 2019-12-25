using System;
using CardGameAPI.Models;
using CardGameAPI.Repositories.Interface;
using System.Collections.Generic;
using System.Linq;
using NLog;

namespace CardGameAPI.Repositories
{
  public class GameEngine : IGameEngine
  {
    public List<Game> Games;
    public List<Player> Players;
    public List<Card> Cards;
    public readonly List<CardRole> CardRoles;
    private static readonly Random Rng = new Random();
    public readonly EFContext _context;
    public readonly Logger _logger;

    public GameEngine(EFContext context)
    {
      _context = context;
      Games = _context.Games.ToList();
      Players = new List<Player>();
      Cards = _context.Cards.ToList();
      CardRoles = _context.CardRoles.ToList();
      _logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
    }

    public List<Game> GetGames()
    {
      return Games;
    }

    public List<Card> GetCards()
    {
      return Cards;
    }

    public object GetGameState(Game game)
    {
      return null;
    }

    public string GetGameNameById(int gameId)
    {
      return Games.First(g => g.Id.Equals(gameId)).Name;
    }

    public List<Player> GetPlayersInGameById(int gameId)
    {
      try
      {
        return Games.First(g => g.Id.Equals(gameId)).Players.ToList();
      }
      catch(Exception ex)
      {
        _logger.Log(LogLevel.Error, $"GetPlayersInGameById; Error: {ex.Message}");
      }
      return new List<Player>();
    }

    public List<Player> GetLoggedInUsers()
    {
      try
      {
        return Players.ToList();
      }
      catch (Exception ex)
      {
        _logger.Log(LogLevel.Error, $"GetLoggedInUsers; Error: {ex.Message}");
      }
      return new List<Player>();
    }

    public void StartGame(int gameId)
    {
      try
      {
        Game game = Games.First(g => g.Id.Equals(gameId));
        if (game != null)
        {
          game.Active = true;
          game.Cards = ShuffleCards();

          _context.Games.First(g => g.Id.Equals(game.Id)).Active = true;
          _context.SaveChanges();

          foreach (Player p in game.Players)
          {
            p.CurrentGame = game;
            p.LastActivity = DateTime.Now;
            p.Points = 0;
          }
          for (int i = 0; i < 6; i++)
          {
            game.CardPiles.Add(new List<Card>());
          }
          for (int i = 0; i < game.Cards.Count; i++)
          {
            game.CardPiles[i % 6].Add(game.Cards[i]);
          }
        }
      }
      catch (Exception ex)
      {
        _logger.Log(LogLevel.Error, $"StartGame; Error: {ex.Message}");
      }
    }

    public List<Card> ShuffleCards()
    {
      return Cards.OrderBy(a => Rng.Next()).ToList();
    }
  }
}
