using CardGameAPI.Repositories;
using CardGameAPI.Repositories.Interface;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace CardGameAPI.Hubs
{
  public class GameHub : Hub
  {
    readonly IGameEngine _gameEngine;

    public GameHub(GameEngine gameEngine)
    {
      _gameEngine = gameEngine;
    }

    public async Task SendMessage(string user, string message)
    {
      await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public async Task SendLoggedInUsers()
    {
      await Clients.All.SendAsync("ReceiveLoggedInUsers", _gameEngine.GetLoggedInUsers());
    }

    public async Task SendGames()
    {
      await Clients.All.SendAsync("ReceiveGames", _gameEngine.GetGames());
    }
  }
}
