using System;
using System.ComponentModel.DataAnnotations;

namespace CardGame.Models
{
    public class Log
    {
        [Key]
        public int Id { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public string Exception { get; set; }
    }
}
