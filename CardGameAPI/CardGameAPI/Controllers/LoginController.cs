using CardGameAPI.Models;
using CardGameAPI.Repositories;
using CardGameAPI.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CardGameAPI.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class LoginController : ControllerBase
  {
    private readonly Settings _settings;
    private LoginRepository _loginRepository;

    public LoginController(IOptions<Settings> settings)
    {
      _settings = settings.Value;
      _loginRepository = new LoginRepository(_settings);
    }

    [HttpPost]
    public ActionResult<CGMessage> Login(Player player)
    {
      return Ok(_loginRepository.Login(player));
    }
  }

}
