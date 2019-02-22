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

  ngOnInit() {}

  public saveGame() {
    this._gameService.SaveGame(this._selectedGame).subscribe(result => {
      if (result.status === true) {
        this._selectedGame = null;
      }
    });
  }

  public deleteGame(deleteGame: IGame) {
    this._gameService.DeleteGame(deleteGame).subscribe();
  }

  public joinGame(game: IGame) {
    const payLoad = { game: game, player: this._loginService.getPlayer() } as IPlayerGame;
    this._gameService.JoinGame(payLoad).subscribe();
  }

  public leaveGame(game: IGame) {
    const payLoad = { game: game, player: this._loginService.getPlayer() } as IPlayerGame;
    this._gameService.LeaveGame(payLoad).subscribe();
  }

  addGame() {
    this._selectedGame = {} as IGame;
  }

  unselectGame() {
    this._selectedGame = null;
  }

}
