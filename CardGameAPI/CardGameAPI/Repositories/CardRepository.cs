using CardGameAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CardGameAPI.Repositories
{
  public class CardRepository
  {
    private EFContext _context;
    private readonly IHostingEnvironment _hostingEnvironment;
    public CardRepository(EFContext context)
    {
      _context = context;
    }

    public CardRepository(EFContext context, IHostingEnvironment hostingEnvironment) 
    {
      _context = context;
      _hostingEnvironment = hostingEnvironment;
    }

    public CGMessage GetCards()
    {
      CGMessage returnMessage = new CGMessage();
      try
      {
        List<Card> cards = _context.Cards.ToList();
        returnMessage.ReturnData.Add(cards);
        List<CardRole> rolesForLookup = _context.CardRoles.ToList();

        foreach (Card card in cards)
        {
          var rolesWithCard = _context.CardsWithRoles.Where(c => c.CardId.Equals(card.Id));
          if (rolesWithCard != null)
          {
            List<CardsWithRole> definedCardsWithRole = rolesWithCard.ToList();
            foreach (CardsWithRole cwr in definedCardsWithRole)
            {
              card.DefinedDice.Add(rolesForLookup.First(c => c.Id.Equals(cwr.CardRoleId)));
            }
          }
        }
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
          card.Name = inputCard.Name; // Set properties individually to register with EF, otherwise doesn't detect changes
          card.Description = inputCard.Description;
          card.Gold = inputCard.Gold;
          card.ReputationPoints = inputCard.ReputationPoints;

          var deleteCardWithRole = _context.CardsWithRoles.Where(cwr => cwr.CardId.Equals(card.Id)); // Only delete CardswithRoles if card already exists
          _context.RemoveRange(deleteCardWithRole);
          _context.SaveChanges();

        }
        else
        {
          _context.Cards.Add(inputCard);
          _context.SaveChanges();
          card = _context.Cards.FirstOrDefault(c => c.Id.Equals(inputCard.Id));
        }

        if (inputCard.DefinedDice != null)
        {
          foreach (CardRole cr in inputCard.DefinedDice) // Add all the DefinedDice to join table
          {
            _context.CardsWithRoles.Add(new CardsWithRole()
            {
              CardId = card.Id,
              CardRoleId = cr.Id
            });
          }
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
