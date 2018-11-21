using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Data.Entity;

namespace CardGameAPI.Models
{
  public class EFContext : DbContext
  {
    public EFContext(Settings settings)
    {
      Database.Connection.ConnectionString =
        $"Server={settings.Server};Database={settings.Database};User Id={settings.UserId};Password={settings.Password};";
    }

    public DbSet<Player> Players { get; set; }
    public DbSet<Game> Games { get; set; }
  }

  public class Settings
  {
    public string Server { get; set; }
    public string Database { get; set; }
    public string UserId { get; set; }
    public string Password { get; set; }
  }

}
