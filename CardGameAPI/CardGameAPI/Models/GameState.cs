using System.Collections.Generic;

namespace CardGame.Models
{
    public class GameState
    {
        public List<GamePlayer> GamePlayers { get; set; }
        public List<List<Card>> CardPiles { get; set; }
        public GamePlayer CurrentGamePlayer { get; set; }
    }
}