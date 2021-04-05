using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardGame.Models
{
  public class Player
  {
    [Key]
    public int Id { get; set; }
    public string UserName { get; set; }
    public DateTime? LastActivity { get; set; }
    public bool Admin { get; set; }
    public int Wins { get; set; } = 0;
    [NotMapped]
    public int Points { get; set; } = 0;
    [NotMapped]
    public int Gold { get; set; } = 0;
    [NotMapped]
    public bool Connected { get; set; } = true;
    [NotMapped]
    public int Order { get; set; } = 0;
    [NotMapped]
    public bool IsSelectedUser { get; set; } = false;
  }
}
