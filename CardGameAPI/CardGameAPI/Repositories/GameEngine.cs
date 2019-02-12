using CardGameAPI.Hubs;
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

    public List<Player> GetLoggedInUsers()
    {
      return _players.Where(p => p.LastActivity > DateTime.Now.AddSeconds(-30)).ToList();
    }
  }
}
