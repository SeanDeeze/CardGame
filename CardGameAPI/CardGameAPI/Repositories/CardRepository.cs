using System;
using System.Collections.Generic;
using System.Linq;
using CardGame.Models;
using CardGame.Models.dto;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;

namespace CardGame.Repositories
{
    public class CardRepository
    {
        private readonly EFContext _context;
        private readonly Logger _logger;
        private readonly string ClassName = "CardRepository";
        public CardRepository(EFContext context)
        {
            _context = context;
            _logger = LogManager.Setup()
                                .LoadConfigurationFromAppSettings(basePath: AppContext.BaseDirectory)
                                .GetCurrentClassLogger();
        }

        public CGMessage GetCards()
        {
            CGMessage returnMessage = new();
            try
            {
                List<Card> cards = _context.Cards.ToList();
                returnMessage.ReturnData.Add(cards);
                List<CardRole> rolesForLookup = _context.CardRoles.ToList();

                foreach (Card card in cards)
                {
                    IQueryable<CardsWithRole> rolesWithCard = _context.CardsWithRoles.Where(c => c.CardId.Equals(card.Id));
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
                _logger.Log(LogLevel.Error, $"GetCards; Error: {ex.Message}");
            }
            return returnMessage;
        }

        public CGMessage GetCardRoles()
        {
            CGMessage returnMessage = new();
            try
            {
                List<CardRole> cardRoles = _context.CardRoles.ToList();
                returnMessage.ReturnData.Add(cardRoles);
                returnMessage.Status = true;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"GetCardRoles; Error: {ex.Message}");
            }
            return returnMessage;
        }

        public CGMessage SaveCard(Card inputCard)
        {
            CGMessage returnMessage = new();
            try
            {
                Card card = _context.Cards.FirstOrDefault(c => c.Id.Equals(inputCard.Id));
                if (card != null)
                {
                    card.Name = inputCard.Name; // Set properties individually to register with EF, otherwise doesn't detect changes
                    card.Description = inputCard.Description;
                    card.Gold = inputCard.Gold;
                    card.ReputationPoints = inputCard.ReputationPoints;
                    card.Image = inputCard.Image;

                    IQueryable<CardsWithRole> deleteCardWithRole = _context.CardsWithRoles.Where(cwr => cwr.CardId.Equals(card.Id)); // Only delete Cards with Roles if card already exists
                    _context.RemoveRange(deleteCardWithRole);
                    _context.SaveChanges();

                }
                else
                {
                    _context.Cards.Add(inputCard);
                    _context.SaveChanges();
                    card = _context.Cards.FirstOrDefault(c => c.Id.Equals(inputCard.Id));
                }

                if (inputCard.DefinedDice != null && card != null)
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
                else
                {
                    _logger.Log(LogLevel.Error, $"{ClassName}.SaveCard; Defined Dice or Card is null. This is not right!!");
                }
                return GetCards();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"SaveCard; Error: {ex.Message}");
            }
            return returnMessage;
        }

        public CGMessage SaveCardRole(CardRole inputCardRole)
        {
            CGMessage returnMessage = new();
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
                _logger.Log(LogLevel.Error, $"SaveCardRole; Error: {ex.Message}");
            }
            return returnMessage;
        }

        public CGMessage DeleteCard(Card inputCard)
        {
            CGMessage returnMessage = new();
            try
            {
                _context.Cards.Remove(inputCard);
                _context.SaveChanges();

                return GetCards();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"DeleteCard; Error: {ex.Message}");
            }
            return returnMessage;
        }

        public CGMessage DeleteCardRole(CardRole inputCardRole)
        {
            CGMessage returnMessage = new();
            try
            {
                _context.CardRoles.Remove(inputCardRole);
                _context.SaveChanges();

                return GetCardRoles();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"DeleteCardRole; Error: {ex.Message}");
            }
            return returnMessage;
        }
    }
}
