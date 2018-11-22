using CardGameAPI.Models;
using System;
using System.Linq;

namespace CardGameAPI.Repositories
{
  public class LoginRepository
  {
    private EFContext _context;
    public LoginRepository(EFContext context)
    {
      _context = context;
    }

    public CGMessage Login(Player player)
    {
      CGMessage returnMessage = new CGMessage();

      try
      {
        Player currentPlayer = _context.Players.FirstOrDefault(p => p.UserName.ToLower().Equals(player.UserName.Trim().ToLower()));
        if (currentPlayer != null)
        {
          currentPlayer.LastActivity = DateTime.Now;
          _context.SaveChanges();
          returnMessage.ReturnData.Add(currentPlayer);
        }
        else
        {
          player.LastActivity = DateTime.Now;
          _context.Players.Add(player); // Add to db, populate db-set insertion values
          _context.SaveChanges();
          currentPlayer = _context.Players.FirstOrDefault(p => p.UserName.ToLower().Equals(player.UserName.Trim().ToLower())); // Reselect with new data
          returnMessage.ReturnData.Add(currentPlayer);
        }
        returnMessage.Status = true;
      }
      catch (Exception ex)
      {
        // Do nothing for now, logger still needs to be implemented
      }
      return returnMessage;
    }
  }
}
