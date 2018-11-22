import { Component, OnInit } from '@angular/core';
import { IGame } from '../shared/models/game';

@Component({
  selector: 'app-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.css']
})
export class GamesComponent implements OnInit {
  games: IGame[] = [];
  constructor() { }

  ngOnInit() {
  }

}
