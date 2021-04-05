using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardGame.Models
{
  public class Game
  {
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; } = false;
    public bool Finished { get; set; } = false;
    [NotMapped]
    public List<Player> Players { get; set; } = new List<Player>();
    [NotMapped]
    public List<Card> Cards { get; set; } = new List<Card>();
  }

  public class PlayerGame
  {
    public Game Game;
    public Player Player;
  }
}
