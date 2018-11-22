using System.ComponentModel.DataAnnotations;

namespace CardGameAPI.Models
{
  public class Card
  {
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
  }
}
