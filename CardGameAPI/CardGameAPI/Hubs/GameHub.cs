using System.Linq;
using CardGameAPI.Models;
using CardGameAPI.Repositories;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace CardGameAPI.Hubs
{
  public class GameHub : Hub
  {
    private readonly IGameEngine _gameEngine;

    public GameHub(IGameEngine gameEngine)
    {
      _gameEngine = gameEngine;
    }

    public async Task SendLoggedInUsers()
    {
      await Clients.All.SendAsync("ReceiveLoggedInUsers  ", _gameEngine.GetLoggedInUsers());
    }

    public async Task SendGames()
    {
      await Clients.All.SendAsync("ReceiveGames", _gameEngine.GetGames());
    }

    public async Task AddToGroup(int groupId)
    {
      string groupName = _gameEngine.GetGameNameById(groupId);
      await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
      await Clients.Group(groupName).SendAsync("ReceiveGameUsers", _gameEngine.GetPlayersInGameById(groupId));
    }

    public async Task RemoveFromGroup(int groupId)
    {
      string groupName = _gameEngine.GetGameNameById(groupId);
      await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
      await Clients.Group(groupName).SendAsync("ReceiveGameUsers", _gameEngine.GetPlayersInGameById(groupId));
    }

    public async Task SendGameState(int gameId)
    {
      string groupName = _gameEngine.GetGameNameById(gameId);
      Game game = _gameEngine.GetGames().FirstOrDefault(g => g.Id == gameId);
      await Clients.Group(groupName).SendAsync("ReceiveGameState", game);
    }
  }
}
