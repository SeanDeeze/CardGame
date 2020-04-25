using System.Collections.Generic;

namespace CardGameAPI.Models.Dto
{
  public class CGMessage
  {
    public bool Status { get; set; } = false;
    public string Message { get; set; }
    public List<object> ReturnData { get; set; } = new List<object>();
  }
}
