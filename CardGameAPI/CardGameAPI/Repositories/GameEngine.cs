using System;
using CardGameAPI.Models;
using System.Collections.Generic;
using System.Linq;
using NLog;
using CardGameAPI.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

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
      Players = new List<Player>();
      Cards = Context.Cards.ToList();
      CardRoles = Context.CardRoles.ToList();
      Logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetLogger("allfile");
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

    public Game GetGameState(int gameId)
    {
      try
      {
        return Games.FirstOrDefault(g => g.Id == gameId);
      }
      catch (Exception ex)
      {
        Logger.Log(LogLevel.Error, $"GameEngine.GetGameState; Error: {ex.Message}");
      }
      return null;
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
      try
      {
        return Games.First(g => g.Id.Equals(gameId)).Players.ToList();
      }
      catch (Exception ex)
      {
        Logger.Log(LogLevel.Error, $"GetPlayersInGameById; Error: {ex.Message}");
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
        Logger.Log(LogLevel.Error, $"GetLoggedInUsers; Error: {ex.Message}");
      }
      return new List<Player>();
    }

    public async System.Threading.Tasks.Task StartGameAsync(int gameId, IHubContext<GameHub> gameHub)
    {
      try
      {
        Game game = Games.First(g => g.Id.Equals(gameId));
        if (game != null)
        {
          game.Active = true;
          game.Cards = ShuffleCards(game.Cards);
          game.Players = ShufflePlayers(game.Players);
          game.Active = true;
          Context.Games.Update(game);
          await Context.SaveChangesAsync();

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

          await gameHub.Clients.Group(GetGameNameById(game.Id)).SendAsync("ReceiveGameState", game);
        }
      }
      catch (Exception ex)
      {
        Logger.Log(LogLevel.Error, $"GameEngine.StartGame; Error: {ex.Message}");
      }
    }

    public async System.Threading.Tasks.Task EndGameAsync(Game game, IHubContext<GameHub> gameHub)
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
            await Context.SaveChangesAsync();

            Games.First(g => g.Id.Equals(game.Id)).Active = false;
            Games.First(g => g.Id.Equals(game.Id)).Finished = true;

            await gameHub.Clients.Group(GetGameNameById(game.Id)).SendAsync("ReceiveGameState", currentGame);
          }
          else
          {
            await gameHub.Clients.Group(GetGameNameById(game.Id)).SendAsync("ReceiveGameState", game);
          }
        }
      }
      catch (Exception ex)
      {
        Logger.Log(LogLevel.Error, $"GameEngine.StartGame; Error: {ex.Message}");
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
