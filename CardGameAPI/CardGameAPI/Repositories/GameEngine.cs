using CardGameAPI.Models;
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
    void AddPlayer(Player p, Game g);
    void RemovePlayer(Player p, Game g);
    void AddGame(Game game);
    void RemoveGame(Game game);
    bool StartGame(int gameId);
    bool EndGame(Game game);
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
      Games = Context.Games.ToList();
      Cards = Context.Cards.ToList();
      CardRoles = Context.CardRoles.ToList();
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

    public void AddPlayer(Player p, Game g)
    {
      try
      {
        Game game = this.Games.FirstOrDefault(sGame => sGame.Id == g.Id);
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
      }
      catch (Exception ex)
      {
        Logger.Log(LogLevel.Error, $"GameEngine.AddPlayer; Error: {ex.Message}");
      }
    }

    public void RemovePlayer(Player p, Game g)
    {

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

    public bool StartGame(int gameId)
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

          for (int i = 0; i < game.Players.Count; i++)
          {
            Player p = game.Players[i];
            if (i == 0)
            {
              p.IsSelectedUser = true;
            }
            p.LastActivity = DateTime.Now;
            p.Points = 0;
            p.Gold = 0;
            p.Order = i;
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

    public bool EndGame(Game game)
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
