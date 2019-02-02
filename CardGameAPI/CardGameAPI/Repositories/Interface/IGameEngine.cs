using CardGameAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace CardGameAPI.Repositories.Interface
{
  public interface IGameEngine
  {
    List<Game> GetGames();
    List<Player> GetLoggedInUsers();
  }
}
