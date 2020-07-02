import { Component, OnInit } from '@angular/core';
import { IGame, IPlayerGame } from '../shared/models/game';
import { GameService } from '../services/game.service';
import { LoginService } from '../services/login.service';
import { SignalRService } from '../services/signal-r.service';
import { Router } from '@angular/router';
import { Subscription, timer } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { CGMessage } from '../shared/models/CGMessage';

@Component({
  selector: 'app-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.css']
})
export class GamesComponent implements OnInit {
  games: IGame[] = [];
  userGame: IGame;
  source: Subscription;
  _selectedGame: IGame;
  constructor(private _gameService: GameService, private _loginService: LoginService, public _signalRService: SignalRService,
    private router: Router) { }

  ngOnInit() {
    this._gameService.IsPlayerInGame(this._loginService.getPlayer())
      .subscribe(response => {
        if (response.returnData[0] != null) {
          return this.router.navigateByUrl('/game');
        } else {
          this.source = timer(0, 2000).pipe(
            switchMap(() => this._gameService.GetGames())
          ).subscribe((result: CGMessage) => {
            if (result.status === true) {
              this.games = result.returnData[0] as IGame[];
            }
          });
        }
      });
  }

  public saveGame() {
    this._gameService.SaveGame(this._selectedGame).subscribe(result => {
      if (result.status === true) {
        this._selectedGame = null;
      }
    });
  }

  public deleteGame(deleteGame: IGame) {
    this._gameService.DeleteGame(deleteGame).subscribe(result => {
      this._signalRService.getEngineGames();
    });
  }

  public joinGame(game: IGame) {
    const payLoad = { game: game, player: this._loginService.getPlayer() } as IPlayerGame;
    this._gameService.JoinGame(payLoad).subscribe(result => {
      if (result.status === true) {
        this.router.navigateByUrl('/game');
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
