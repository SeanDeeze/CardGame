using CardGameAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace CardGameAPI.Repositories.Interface
{
  public interface IGameEngine
  {
    List<Game> GetGames();
    string GetGameNameById(int id);
    List<Player> GetLoggedInUsers();
    List<Player> GetPlayersInGameById(int gameId);
  }
}
