using CardGameAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGameAPI.Repositories
{
  public class CardRepository
  {
    private EFContext _context;
    public CardRepository(EFContext context)
    {
      _context = context;
    }

    public CGMessage GetCards()
    {
      CGMessage returnMessage = new CGMessage();
      try
      {
        List<Card> cards = _context.Cards.ToList();
        returnMessage.ReturnData.Add(cards);
        returnMessage.Status = true;
      }
      catch (Exception ex)
      {
        // Do nothing for now, logger still needs to be implemented
      }
      return returnMessage;
    }

    public CGMessage SaveCard(Card inputCard)
    {
      CGMessage returnMessage = new CGMessage();
      try
      {
        Card card = _context.Cards.FirstOrDefault(c => c.Id.Equals(inputCard.Id));
        if (card != null)
        {
          card.Name = inputCard.Name;
          card.Description = inputCard.Description;
          _context.SaveChanges();
          returnMessage.ReturnData.Add(card);
        }
        else
        {
          _context.Cards.Add(inputCard);
          _context.SaveChanges();
          card = _context.Cards.FirstOrDefault(c => c.Name.Equals(card.Name));
          returnMessage.ReturnData.Add(card);
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
