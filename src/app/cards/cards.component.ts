import { Component, OnInit } from '@angular/core';
import { ICard, ICardRole } from '../shared/models/card';
import { CardService } from '../services/card.service';
import { isNullOrUndefined, isNull } from 'util';
import { SelectItem } from 'primeng/api';

@Component({
  selector: 'app-cards',
  templateUrl: './cards.component.html',
  styleUrls: ['./cards.component.css']
})
export class CardsComponent implements OnInit {
  cards: ICard[] = [];
  selectedCard: ICard;
  selectListCards: ICard[];

  cardRoles: ICardRole[] = [];
  cardRoleSelectItems: SelectItem[] = [];
  associationCardRole: SelectItem = {} as SelectItem;

  selectedRole: ICardRole;
  constructor(private _cardService: CardService) { }

  ngOnInit() {
    this._cardService.GetCards().subscribe(result => {
      if (result.status === true) {
        this.cards = !isNullOrUndefined(result.returnData[0]) ? result.returnData[0] as ICard[] : [];
      }
    });

    this._cardService.GetCardRoles().subscribe(result => {
      if (result.status === true) {
        this.cardRoles = !isNullOrUndefined(result.returnData[0]) ? result.returnData[0] as ICardRole[] : [];
        this.updateCardRoleSelectItems();
        if (this.cardRoleSelectItems.length > 0) {
          this.associationCardRole = this.cardRoleSelectItems[0];
        }
      }
    });
  }

  saveCard() {
    this._cardService.SaveCard(this.selectedCard).subscribe(result => {
      if (result.status === true) {
        this.cards = !isNullOrUndefined(result.returnData[0]) ? result.returnData[0] as ICard[] : [];
        this.selectedCard = null;
        this.updateCardRoleSelectItems();
      }
    });
  }

  saveCardRole() {
    this._cardService.SaveCardRole(this.selectedRole).subscribe(result => {
      if (result.status === true) {
        this.cardRoles = !isNullOrUndefined(result.returnData[0]) ? result.returnData[0] as ICardRole[] : [];
        this.selectedRole = null;
        this.updateCardRoleSelectItems();
      }
    });
  }

  deleteCard(card: ICard) {
    this._cardService.DeleteCard(card).subscribe(result => {
      if (result.status === true) {
        this.cards = !isNullOrUndefined(result.returnData[0]) ? result.returnData[0] as ICard[] : [];
        this.updateCardRoleSelectItems();
      }
    });
  }

  deleteCardRole(cardRole: ICardRole) {
    this._cardService.DeleteCardRole(cardRole).subscribe(result => {
      if (result.status === true) {
        this.cardRoles = !isNullOrUndefined(result.returnData[0]) ? result.returnData[0] as ICardRole[] : [];
        this.updateCardRoleSelectItems();
      }
    });
  }

  addCard() {
    this.selectedCard = {} as ICard;
    if (isNullOrUndefined(this.selectedCard.definedDice)) {
      this.selectedCard.definedDice = [];
    }
  }

  editCard(card: ICard) {
    this.selectedCard = card;
    if (isNullOrUndefined(this.selectedCard.definedDice)) {
      this.selectedCard.definedDice = [];
    }
  }

  addRoleToCard() {
    if (!isNullOrUndefined(this.associationCardRole)) {
      this.selectedCard.definedDice.push(this.associationCardRole.value);
    }
  }

  removeRoleFromCard(roleToRemove: ICardRole) {
    if (!isNullOrUndefined(roleToRemove)) {
      const roleIndex = this.selectedCard.definedDice.indexOf(roleToRemove);
      if (roleIndex > -1) {
        this.selectedCard.definedDice.splice(roleIndex, 1);
      }
    }
  }

  addCardRole() {
    this.selectedRole = {} as ICardRole;
  }

  editCardRole(cardRole: ICardRole) {
    this.selectedRole = cardRole;
  }

  unselectCard() {
    this.selectedCard = null;
  }

  unselectRole() {
    this.selectedRole = null;
  }

  updateCardRoleSelectItems() {
    this.cardRoleSelectItems = this.cardRoles.map(element => {
      return { label: element.name, value: element } as SelectItem;
    });
  }
}
