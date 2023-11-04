using CardGame.Models.dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CardGame.Models
{
    public class User
    {
        [Key]
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public DateTimeOffset LastActivity { get; set; }
        public bool Admin { get; set; }
        public int Wins { get; set; } = 0;
    }

    // Used to store a player's information in a game
    public class GamePlayer
    {
        public User Player { get; set; }
        public int Gold { get; set; } = 0;
        public int ReputationPoints { get; set; } = 0;
        public List<Card> Cards { get; set; } = new();
        public int Order { get; set; }
        public List<Dice> Dice { get; set; }
        public bool Leader { get; set; }
    }
}
