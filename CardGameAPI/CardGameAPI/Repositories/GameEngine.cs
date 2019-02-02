using CardGameAPI.Models;
using CardGameAPI.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGameAPI.Repositories
{
  public class GameEngine : IGameEngine
  {
    private static readonly GameEngine _gameEngine = new GameEngine();
    private EFContext _context;
    private readonly List<Game> _games;
    private List<Player> _players;
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

    public GameEngine()
    {
      _games = new List<Game>();
      _players = new List<Player>();
      _cards = new List<Card>();
      _cardRoles = new List<CardRole>();
    }

    public List<Game> GetGames()
    {
      return _games;
    }

    public List<Player> GetLoggedInUsers()
    {
      return _gameEngine._players.Where(p => p.LastActivity != DateTime.MinValue).ToList();
    }
  }
}
