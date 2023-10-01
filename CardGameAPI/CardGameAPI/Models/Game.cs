using CardGame.Repositories;
using NLog;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardGame.Models
{
    public class Game
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; } = false;

        [NotMapped]
        public GameEngine Engine { get; set; }

        public Game() { }

        public Game(Game game, Logger logger)
        {
            ID = game.ID;
            Name = game.Name;
            Active = game.Active;
            Engine = new GameEngine(logger);
        }
    }

    // Used to let a Player join an existing Game
    public class PlayerGame
    {
        public Game Game { get; set; }
        public User Player { get; set; }
    }
}
