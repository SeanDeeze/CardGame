using System;
using System.Collections.Generic;

namespace CardGame.Models
{
    public class GameState
    {
        public bool Started { get; set; }
        public List<GamePlayer> GamePlayers { get; set; }
        public List<Card> Cards { get; set; }
        public Guid? CurrentGamePlayerID { get; set; } // May be null if the game hasn't started yet
    }
}