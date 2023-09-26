using CardGame.Repositories;
using NLog;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardGame.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; } = false;

        [NotMapped]
        public GameEngine Engine { get; set; }

        public Game(Logger logger)
        {
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
