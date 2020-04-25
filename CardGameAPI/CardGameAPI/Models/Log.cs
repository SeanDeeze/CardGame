using System;
using System.ComponentModel.DataAnnotations;

namespace CardGameAPI.Models
{
  public class Log
  {
    [Key]
    public int Id { get; set; }
    public string Level { get; set; }
    public string Logger { get; set; }
    public string Message { get; set; }
    public DateTime TimeStamp { get; set; }

  }
}
