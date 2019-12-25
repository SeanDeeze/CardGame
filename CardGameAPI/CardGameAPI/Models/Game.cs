using System.Collections.Generic;
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
    public bool Finished { get; set; }
    [NotMapped]
    public List<Player> Players { get; set; } = new List<Player>();
    [NotMapped]
    public List<Card> Cards { get; set; } = new List<Card>();
    public List<List<Card>> CardPiles = new List<List<Card>>();
  }

  public class PlayerGame
  {
    public Game game;
    public Player player;
  }
}
