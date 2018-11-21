using System;
using System.Collections.Generic;

namespace CardGameAPI.Models
{
  public class CGMessage
  {
    public bool Status { get; set; } = false;
    public string Message { get; set; }
    public List<Object> ReturnData { get; set; }
  }
}
