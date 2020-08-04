using CardGameAPI.Models;
using CardGameAPI.Models.Dto;
using CardGameAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CardGameAPI.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly UserRepository _userRepository;

    public UserController(EFContext context, IGameEngine gameEngine, ILogger<LoginController> logger)
    {
      _userRepository = new UserRepository(context, gameEngine, logger);
    }

    [HttpGet]
    public CGMessage GetUsers()
    {
      return _userRepository.GetUsers();
    }
  }

}
