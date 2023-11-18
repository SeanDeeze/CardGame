using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardGame.Models
{
    public class Card
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ReputationPoints { get; set; }
        public int Gold { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public List<CardRole> CardRoles { get; set; } = [];
        [NotMapped] public int PileNumber { get; set; } = 0;
    }

    public class CardRole
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int DiceNumber { get; set; }
    }

    public class CardsWithRole
    {
        [Key]
        public int Id { get; set; }
        public int CardId { get; set; }
        public int CardRoleId { get; set; }
    }
}
