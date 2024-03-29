using Microsoft.EntityFrameworkCore;

namespace CardGame.Models
{
    public class EFContext(DbContextOptions<EFContext> dbContextOptions) : DbContext(dbContextOptions)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<CardRole> CardRoles { get; set; }
        public DbSet<CardsWithRole> CardsWithRoles { get; set; }
        public DbSet<Log> Logs { get; set; }
    }

    public class Settings
    {
        // Placeholder for future appSettings.Config based settings
    }

}
