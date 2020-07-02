using CardGameAPI.Hubs;
using CardGameAPI.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGameAPI.Repositories
{
  public interface IGameEngine
  {
    List<Player> GetPlayers();
    List<Game> GetGames();
    List<Card> GetCards();
    void AddGame(Game game);
    void RemoveGame(Game game);
    bool StartGame(int gameId, IHubContext<GameHub> gameHub);
    bool EndGame(Game game, IHubContext<GameHub> gameHub);
    string GetGameNameById(int id);
    List<Player> GetLoggedInUsers();
    List<Player> GetPlayersInGameById(int gameId);
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

    public GameEngine(EFContext context)
    {
      Context = context;
      Games = Context.Games.AsNoTracking().ToList();
      Cards = Context.Cards.AsNoTracking().ToList();
      CardRoles = Context.CardRoles.AsNoTracking().ToList();
      Players = new List<Player>();
      Logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetLogger("allfile");
    }

    public List<Player> GetPlayers()
    {
      List<Player> returnPlayers = new List<Player>();
      try
      {
        returnPlayers = Players;
      }
      catch (Exception ex)
      {
        Logger.Log(LogLevel.Error, $"GameEngine.GetPlayers; Error: {ex.Message}");
      }

      return returnPlayers;
    }

    public List<Game> GetGames()
    {
      try
      {
        return Games.ToList();
      }
      catch (Exception ex)
      {
        Logger.Log(LogLevel.Error, $"GameEngine.GetGames; Error: {ex.Message}");
      }
      return new List<Game>();
    }

    public void AddGame(Game game)
    {
      try
      {
        Games.Add(game);
      }
      catch (Exception ex)
      {
        Logger.Log(LogLevel.Error, $"GameEngine.AddGame; Error: {ex.Message}");
      }
    }

    public void RemoveGame(Game game)
    {
      try
      {
        Games.Remove(game);
      }
      catch (Exception ex)
      {
        Logger.Log(LogLevel.Error, $"GameEngine.RemoveGame; Error: {ex.Message}");
      }
    }

    public List<Card> GetCards()
    {
      try
      {
        return Cards;
      }
      catch (Exception ex)
      {
        Logger.Log(LogLevel.Error, $"GameEngine.GetCards; Error: {ex.Message}");
      }
      return new List<Card>();
    }

    public string GetGameNameById(int gameId)
    {
      try
      {
        return Games.First(g => g.Id.Equals(gameId)).Name;
      }
      catch (Exception ex)
      {
        Logger.Log(LogLevel.Error, $"GameEngine.GetGameNameById; Error: {ex.Message}");
      }
      return string.Empty;
    }

    public List<Player> GetPlayersInGameById(int gameId)
    {
      List<Player> returnList = new List<Player>();
      try
      {
        Game checkGame = Games.FirstOrDefault(g => g.Id.Equals(gameId));
        if (checkGame != null)
        {
          returnList = checkGame.Players.ToList();
        }
      }
      catch (Exception ex)
      {
        Logger.Log(LogLevel.Error, $"GetPlayersInGameById; Error: {ex.Message}");
      }

      return returnList;
    }

    public List<Player> GetLoggedInUsers()
    {
      try
      {
        return Players.ToList();
      }
      catch (Exception ex)
      {
        Logger.Log(LogLevel.Error, $"GetLoggedInUsers; Error: {ex.Message}");
      }
      return new List<Player>();
    }

    public bool StartGame(int gameId, IHubContext<GameHub> gameHub)
    {
      try
      {
        Game game = Games.First(g => g.Id.Equals(gameId));
        Game updateGame = game;
        if (game != null)
        {
          game.Active = true;
          game.Cards = ShuffleCards(game.Cards);
          game.Players = ShufflePlayers(game.Players);
          game.Active = true;
          Context.Games.Update(game);
          Context.SaveChanges();

          foreach (Player p in game.Players)
          {
            p.CurrentGame = game;
            p.LastActivity = DateTime.Now;
            p.Points = 0;
            p.Gold = 0;
          }
          for (int i = 0; i < game.Cards.Count; i++)
          {
            game.Cards[i].CardPile = i % 6;
          }

          Games[Games.IndexOf(updateGame)] = game;
        }
      }
      catch (Exception ex)
      {
        Logger.Log(LogLevel.Error, $"GameEngine.StartGame; Error: {ex.Message}");
        return false;
      }

      return true;
    }

    public bool EndGame(Game game, IHubContext<GameHub> gameHub)
    {
      try
      {
        if (game != null)
        {
          Game currentGame = Context.Games.FirstOrDefault(g => g.Id == game.Id);
          if (currentGame != null)
          {
            game.Active = false;
            game.Finished = true;
            Context.Entry(currentGame).CurrentValues.SetValues(game);
            Context.SaveChanges();

            Games.First(g => g.Id.Equals(game.Id)).Active = false;
            Games.First(g => g.Id.Equals(game.Id)).Finished = true;

            _ = gameHub.Clients.Group(GetGameNameById(game.Id)).SendAsync("ReceiveGameState", currentGame);
          }
          else
          {
            _ = gameHub.Clients.Group(GetGameNameById(game.Id)).SendAsync("ReceiveGameState", game);
          }
        }
      }
      catch (Exception ex)
      {
        Logger.Log(LogLevel.Error, $"GameEngine.StartGame; Error: {ex.Message}");
        return false;
      }

      return true;
    }

    public List<Card> ShuffleCards(List<Card> cards)
    {
      try
      {
        return cards.OrderBy(a => Rng.Next()).ToList();
      }
      catch (Exception ex)
      {
        Logger.Log(LogLevel.Error, $"GameEngine.ShuffleCards; Error: {ex.Message}");
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
        Logger.Log(LogLevel.Error, $"GameEngine.ShuffleCards; Error: {ex.Message}");
      }
      return new List<Player>();
    }
  }
}
