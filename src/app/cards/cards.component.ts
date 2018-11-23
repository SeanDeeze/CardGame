import { Component, OnInit } from '@angular/core';
import { ICard } from '../shared/models/card';
import { CardService } from '../services/card.service';

@Component({
  selector: 'app-cards',
  templateUrl: './cards.component.html',
  styleUrls: ['./cards.component.css']
})
export class CardsComponent implements OnInit {
  cards: ICard[] = [];
  constructor(private _cardService: CardService) { }

  ngOnInit() {
  }

}
