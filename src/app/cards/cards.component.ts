import { Component, OnInit } from '@angular/core';
import { ICard, ICardRole } from '../shared/models/card';
import { CardService } from '../services/card.service';
import { isNullOrUndefined } from 'util';

@Component({
  selector: 'app-cards',
  templateUrl: './cards.component.html',
  styleUrls: ['./cards.component.css']
})
export class CardsComponent implements OnInit {
  cards: ICard[] = [];
  _selectedCard: ICard;
  cardRoles: ICardRole[] = [];
  _selectedRole: ICardRole;
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
      }
    });
  }

  saveCard() {
    this._cardService.SaveCard(this._selectedCard).subscribe(result => {
      if (result.status === true) {
        this.cards = !isNullOrUndefined(result.returnData[0]) ? result.returnData[0] as ICard[] : [];
        this._selectedCard = null;
      }
    });
  }

  saveCardRole() {
    this._cardService.SaveCardRole(this._selectedRole).subscribe(result => {
      if (result.status === true) {
        this.cardRoles = !isNullOrUndefined(result.returnData[0]) ? result.returnData[0] as ICardRole[] : [];
        this._selectedRole = null;
      }
    });
  }

  deleteCard(card: ICard) {
    this._cardService.DeleteCard(card).subscribe(result => {
      if (result.status === true) {
        this.cards = !isNullOrUndefined(result.returnData[0]) ? result.returnData[0] as ICard[] : [];
      }
    });
  }

  deleteCardRole(cardRole: ICardRole) {
    this._cardService.DeleteCardRole(cardRole).subscribe(result => {
      if (result.status === true) {
        this.cardRoles = !isNullOrUndefined(result.returnData[0]) ? result.returnData[0] as ICardRole[] : [];
      }
    });
  }

  addCard() {
    this._selectedCard = {} as ICard;
  }

  editCard(card: ICard) {
    this._selectedCard = card;
  }

  addCardRole() {
    this._selectedRole = {} as ICardRole;
  }

  editCardRole(cardRole: ICardRole) {
    this._selectedRole = cardRole;
  }

  unselectCard() {
    this._selectedCard = null;
  }

  unselectRole() {
    this._selectedRole = null;
  }


}
