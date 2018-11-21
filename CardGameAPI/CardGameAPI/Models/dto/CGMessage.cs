using System;
using System.Linq;

namespace CardGameAPI.Models
{
  public class CGMessage
  {
    public bool Status { get; set; }
    public string Message { get; set; }
    public IQueryable<Object> ReturnData { get; set; }
  }
}
