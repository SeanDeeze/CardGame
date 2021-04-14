using CardGame.Models;
using CardGame.Models.dto;
using CardGame.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CardGame.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly UserRepository _userRepository;

    public UserController(EFContext context, IGameEngine gameEngine, ILogger<UserController> logger)
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
