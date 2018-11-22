import { Component, OnInit } from '@angular/core';
import { ICard } from '../shared/models/card';

@Component({
  selector: 'app-cards',
  templateUrl: './cards.component.html',
  styleUrls: ['./cards.component.css']
})
export class CardsComponent implements OnInit {
  cards: ICard[] = [];
  constructor() { }

  ngOnInit() {
  }

}
