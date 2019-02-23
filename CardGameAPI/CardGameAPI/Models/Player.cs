using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardGameAPI.Models
{
  public class Player
  {
    [Key]
    public int? Id { get; set; }
    public string UserName { get; set; }
    public DateTime? LastActivity { get; set; }
    public bool Admin { get; set; }
    public int Wins { get; set; } = 0;
    public int Points { get; set; } = 0;
    [NotMapped]
    public Game CurrentGame { get; set; }
  }
}
