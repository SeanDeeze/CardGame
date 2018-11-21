using System.ComponentModel.DataAnnotations;

namespace CardGameAPI.Models
{
  public class Game
  {
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public bool InProgress { get; set; }
  }
}
