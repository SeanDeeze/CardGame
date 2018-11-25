using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardGameAPI.Models
{
  public class Card
  {
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    [NotMapped]
    public List<CardsWithRole> DefinedDice { get; set; }
  }

  public class CardRole
  {
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public int DiceNumber { get; set; }
  }

  public class CardsWithRole
  {
    [Key]
    public int Id { get; set; }
    public int CardId { get; set; }
    public int CardRoleId { get; set; }
  }
}
