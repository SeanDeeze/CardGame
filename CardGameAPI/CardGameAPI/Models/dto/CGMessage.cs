using System.Collections.Generic;

namespace CardGame.Models.dto
{
    public class CGMessage
    {
        public bool Status { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public List<object> ReturnData { get; set; } = new();
    }
}
