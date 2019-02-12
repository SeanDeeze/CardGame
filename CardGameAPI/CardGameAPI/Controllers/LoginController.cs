using CardGameAPI.Models;
using CardGameAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CardGameAPI.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class LoginController : ControllerBase
  {
    private readonly EFContext _context;
    private readonly GameEngine _gameEngine;
    private LoginRepository _loginRepository;

    public LoginController(EFContext context, GameEngine gameEngine)
    {
      _context = context;
      _gameEngine = gameEngine;
      _loginRepository = new LoginRepository(_context, _gameEngine);
    }

    [HttpPost]
    public CGMessage Login(Player player)
    {
      return _loginRepository.Login(player);
    }

    [HttpPost]
    public CGMessage Logout(Player player)
    {
      return _loginRepository.Logout(player);
    }

    [HttpPost]
    public CGMessage KeepAlive(Player player)
    {
      return _loginRepository.KeepAlive(player);
    }
  }

}
