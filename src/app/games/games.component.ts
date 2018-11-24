import { Component, OnInit } from '@angular/core';
import { IGame } from '../shared/models/game';
import { GameService } from '../services/game.service';
import { isNullOrUndefined } from 'util';

@Component({
  selector: 'app-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.css']
})
export class GamesComponent implements OnInit {
  games: IGame[] = [];
  _selectedGame: IGame;
  constructor(private _gameService: GameService) { }

  ngOnInit() {
    this._gameService.GetGames().subscribe(result => {
      if (result.status === true) {
        this.games = !isNullOrUndefined(result.returnData[0]) ? result.returnData[0] as IGame[] : [];
      }
    });
  }

  public saveGame() {
    this._gameService.SaveGame(this._selectedGame).subscribe(result => {
      if (result.status === true) {
        this.games = !isNullOrUndefined(result.returnData[0]) ? result.returnData[0] as IGame[] : [];
        this._selectedGame = null;
      }
    });
  }

  public deleteGame(deleteGame: IGame) {
    this._gameService.DeleteGame(deleteGame).subscribe(result => {
      if (result.status === true) {
        this.games = !isNullOrUndefined(result.returnData[0]) ? result.returnData[0] as IGame[] : [];
      }
    });
  }

  addGame() {
    this._selectedGame = {} as IGame;
  }

  unselectGame() {
    this._selectedGame = null;
  }

}
