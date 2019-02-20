import { Component, OnInit } from '@angular/core';
import { IGame, IPlayerGame } from '../shared/models/game';
import { GameService } from '../services/game.service';
import { isNullOrUndefined } from 'util';
import { LoginService } from '../services/login.service';
import { SignalRService } from '../services/signal-r.service';

@Component({
  selector: 'app-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.css']
})
export class GamesComponent implements OnInit {
  games: IGame[] = [];
  _selectedGame: IGame;
  constructor(private _gameService: GameService, private _loginService: LoginService, public _signalRService: SignalRService) { }

  ngOnInit() {
    this.getGames();
  }

  getGames() {
    this._gameService.GetGames().subscribe(result => {
      if (result.status === true) {
        this.games = !isNullOrUndefined(result.returnData[0]) ? result.returnData[0] as IGame[] : [];
        this.games.forEach(game => {
          game.active = game.players.length > 0;
        });
      }
    });
  }

  public saveGame() {
    this._gameService.SaveGame(this._selectedGame).subscribe(result => {
      if (result.status === true) {
        this._selectedGame = null;
        this.getGames();
      }
    });
  }

  public deleteGame(deleteGame: IGame) {
    this._gameService.DeleteGame(deleteGame).subscribe(result => {
      if (result.status === true) {
        this.getGames();
      }
    });
  }

  public joinGame(game: IGame) {
    const payLoad = { game: game, player: this._loginService.getPlayer() } as IPlayerGame;
    this._gameService.JoinGame(payLoad).subscribe(result => {
      if (result.status === true) {
        this.getGames();
      }
    });
  }

  public leaveGame(game: IGame) {
    const payLoad = { game: game, player: this._loginService.getPlayer() } as IPlayerGame;
    this._gameService.LeaveGame(payLoad).subscribe(result => {
      if (result.status === true) {
        this.getGames();
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
