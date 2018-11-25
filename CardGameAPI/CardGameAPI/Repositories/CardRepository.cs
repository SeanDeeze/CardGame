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

    public CGMessage GetCardRoles()
    {
      CGMessage returnMessage = new CGMessage();
      try
      {
        List<CardRole> cardRoles = _context.CardRoles.ToList();
        returnMessage.ReturnData.Add(cardRoles);
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

          var deleteCardWithRole = _context.CardsWithRoles.Any();
          _context.RemoveRange(deleteCardWithRole);
          _context.SaveChanges();

          _context.AddRange(inputCard.DefinedDice);
          _context.SaveChanges();
        }
        else
        {
          _context.Cards.Add(inputCard);
          _context.SaveChanges();
        }
        return GetCards();
      }
      catch (Exception ex)
      {
        // Do nothing for now, logger still needs to be implemented
      }
      return returnMessage;
    }

    public CGMessage SaveCardRole(CardRole inputCardRole)
    {
      CGMessage returnMessage = new CGMessage();
      try
      {
        CardRole cardRole = _context.CardRoles.FirstOrDefault(c => c.Id.Equals(inputCardRole.Id));
        if (cardRole != null)
        {
          cardRole.Name = inputCardRole.Name;
          cardRole.DiceNumber = inputCardRole.DiceNumber;
          _context.SaveChanges();
        }
        else
        {
          _context.CardRoles.Add(inputCardRole);
          _context.SaveChanges();
        }
        return GetCardRoles();
      }
      catch (Exception ex)
      {
        // Do nothing for now, logger still needs to be implemented
      }
      return returnMessage;
    }

    public CGMessage DeleteCard(Card inputCard)
    {
      CGMessage returnMessage = new CGMessage();
      try
      {
        _context.Cards.Remove(inputCard);
        _context.SaveChanges();

        return GetCards();
      }
      catch (Exception ex)
      {
        // Do nothing for now, logger still needs to be implemented
      }
      return returnMessage;
    }

    public CGMessage DeleteCardRole(CardRole inputCardRole)
    {
      CGMessage returnMessage = new CGMessage();
      try
      {
        _context.CardRoles.Remove(inputCardRole);
        _context.SaveChanges();

        return GetCardRoles();
      }
      catch (Exception ex)
      {
        // Do nothing for now, logger still needs to be implemented
      }
      return returnMessage;
    }

  }
}
