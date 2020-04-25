using CardGameAPI.Models;
using CardGameAPI.Models.Dto;
using Microsoft.Extensions.Options;

namespace CardGameAPI.Repositories.Interface
{
  interface ILoginRepository
  {
    CGMessage Login(IOptions<Settings> settings);
  }
}
