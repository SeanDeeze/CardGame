using CardGameAPI.Models;
using CardGameAPI.Repositories;
using CardGameAPI.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace CardGameAPI.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class LoginController : ControllerBase
  {
    private readonly EFContext _context;
    private LoginRepository _loginRepository;

    public LoginController(EFContext context)
    {
      _context = context;
      _loginRepository = new LoginRepository(_context);
    }

    [HttpPost]
    public CGMessage Login(Player player)
    {
      return _loginRepository.Login(player);
    }
  }

}
