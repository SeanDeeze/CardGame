using CardGame.Models;
using CardGame.Models.dto;
using Microsoft.Extensions.Options;

namespace CardGame.Repositories.Interface
{
  interface ILoginRepository
  {
    CGMessage Login(IOptions<Settings> settings);
  }
}
