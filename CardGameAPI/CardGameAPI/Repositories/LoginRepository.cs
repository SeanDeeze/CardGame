using CardGameAPI.Models;
using Microsoft.Extensions.Options;
using System.Data.Entity;

namespace CardGameAPI.Repositories
{
  public class LoginRepository
  {
    private DbContext _context;
    public LoginRepository(Settings settings)
    {
      _context = new EFContext(settings);
    }

    public CGMessage Login(Player player)
    {
      return new CGMessage();
    }
  }
}
