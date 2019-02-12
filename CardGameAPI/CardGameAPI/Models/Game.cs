using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardGameAPI.Models
{
  public class Game
  {
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; }
    [NotMapped]
    public System.Collections.Generic.List<Player> Players { get; set; } = new System.Collections.Generic.List<Player>();
  }

  public class PlayerGame
  {
    public Game game;
    public Player player;
  }
}
