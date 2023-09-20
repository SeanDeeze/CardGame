using CardGame.Models;
using CardGame.Models.dto;
using CardGame.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CardGame.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginRepository _loginRepository;

        public LoginController(EFContext context, IGameEngine gameEngine, ILogger<LoginController> logger)
        {
            _loginRepository = new LoginRepository(context, gameEngine, logger);
        }

        [HttpPost]
        public CGMessage Login(User user)
        {
            return _loginRepository.Login(user);
        }

        [HttpPost]
        public CGMessage Logout(User user)
        {
            return _loginRepository.Logout(user);
        }
    }

}
