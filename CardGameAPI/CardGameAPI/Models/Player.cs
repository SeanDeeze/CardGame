using System;
using System.ComponentModel.DataAnnotations;

namespace CardGameAPI.Models
{
  public class Player
  {
    [Key]
    public int? Id { get; set; }
    public string UserName { get; set; }
    public DateTime? LastActivity { get; set; }
  }
}
