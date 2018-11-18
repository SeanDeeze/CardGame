using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardGameAPI.Models
{
  public class CGMessage
  {
    public bool Status { get; set; }
    public string Message { get; set; }
    public IQueryable<Object> ReturnData { get; set; }
  }
}
