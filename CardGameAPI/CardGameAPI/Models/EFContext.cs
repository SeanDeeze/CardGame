using Microsoft.EntityFrameworkCore;

namespace CardGameAPI.Models
{
  public class EFContext : DbContext
  {
    public EFContext(DbContextOptions<EFContext> dbContextOptions): base(dbContextOptions) {}

    public DbSet<Player> Players { get; set; }
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
