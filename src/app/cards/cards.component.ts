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
  selectedCard: ICard;
  cardRoles: ICardRole[] = [];
  selectedRole: ICardRole;
  constructor(private _cardService: CardService) { }

  ngOnInit() {
    this._cardService.GetCards().subscribe(result => {
      if (result.status === true) {
        this.cards = !isNullOrUndefined(result.returnData[0]) ? result.returnData[0] : [];
      }
    });

    this._cardService.GetCardRoles().subscribe(result => {
      if (result.status === true) {
        this.cardRoles = !isNullOrUndefined(result.returnData[0]) ? result.returnData[0] : [];
      }
    });
  }

  saveCard() {
    this._cardService.SaveCard(this.selectedCard).subscribe(result => {
      if (result.status === true) {
        this.cards = !isNullOrUndefined(result.returnData[0]) ? result.returnData[0] : [];
      }
    });
  }

  saveCardRole() {
    this._cardService.SaveCardRole(this.selectedRole).subscribe(result => {
      if (result.status === true) {
        this.cards = !isNullOrUndefined(result.returnData[0]) ? result.returnData[0] : [];
      }
    });
  }

  addCard() {
    this.selectedCard = {} as ICard;
  }

  addCardRole() {
    this.selectedRole = {} as ICardRole;
  }

  unselectCard() {
    this.selectedCard = null;
  }

  unselectRole() {
    this.selectedRole = null;
  }


}
