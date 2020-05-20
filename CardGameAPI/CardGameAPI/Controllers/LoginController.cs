using System.Threading.Tasks;
using CardGameAPI.Hubs;
using CardGameAPI.Models;
using CardGameAPI.Models.Dto;
using CardGameAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace CardGameAPI.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class LoginController : ControllerBase
  {
    private readonly LoginRepository _loginRepository;

    public LoginController(EFContext context, IGameEngine gameEngine, IHubContext<GameHub> gameHub, ILogger<LoginController> logger)
    {
      _loginRepository = new LoginRepository(context, gameEngine, gameHub, logger);
    }

    [HttpPost]
    public Task<CGMessage> Login(Player player)
    {
      return _loginRepository.Login(player);
    }

    [HttpPost]
    public CGMessage Logout(Player player)
    {
      return _loginRepository.Logout(player);
    }
  }

}
