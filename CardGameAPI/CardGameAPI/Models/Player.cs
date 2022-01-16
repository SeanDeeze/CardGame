using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardGame.Models
{
    public class Player
    {
        [Key]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public DateTimeOffset LastActivity { get; set; }
        public bool Admin { get; set; }
        public int Wins { get; set; } = 0;
    }

    // Used to store a player's information in a game
    public class GamePlayer
    {
        public Player Player { get; set; }
        public int Gold { get; set; } = 0;
        public int ReputationPoints { get; set; } = 0;
        public List<Card> Cards { get; set; } = new();
        public int Order { get; set; }
        public List<int> Dice { get; set; }
    }
}
