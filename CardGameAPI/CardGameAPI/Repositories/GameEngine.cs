using CardGameAPI.Models;
using CardGameAPI.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CardGameAPI.Repositories
{
  public class GameEngine : IGameEngine
  {
    private EFContext _context;
    public List<Game> _games;
    public List<Player> _players;
    private readonly List<Card> _cards;
    private readonly List<CardRole> _cardRoles;

    public GameEngine(EFContext context)
    {
      _context = context;
      _games = _context.Games.ToList();
      _players = _context.Players.ToList();
      _cards = _context.Cards.ToList();
      _cardRoles = _context.CardRoles.ToList();
    }

    public List<Game> GetGames()
    {
      return _games;
    }

    public List<Card> GetCards()
    {
      return _cards;
    }

    public string GetGameNameById(int gameId)
    {
      return _games.First(g => g.Id.Equals(gameId)).Name;
    }

    public List<Player> GetPlayersInGameById(int gameId)
    {
      return _games.First(g => g.Id.Equals(gameId)).Players.ToList();
    }

    public List<Player> GetLoggedInUsers()
    {
      return _players.Where(p => p.LastActivity > DateTime.Now.AddMinutes(-120)).ToList();
    }

    public void StartGame(int gameId)
    {
      Game game = _games.First(g => g.Id.Equals(gameId));
      if (game != null)
      {
        game.Active = true;
        game.Cards = _cards;
      }
    }
  }
}
