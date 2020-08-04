import { Component, OnInit, OnDestroy } from '@angular/core';
import { IGame, IPlayerGame } from '../shared/models/game';
import { GameService } from '../services/game.service';
import { LoginService } from '../services/login.service';
import { Router } from '@angular/router';
import { Subscription, timer } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { CGMessage } from '../shared/models/CGMessage';
import { isNullOrUndefined } from 'util';

@Component({
  selector: 'app-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.css']
})
export class GamesComponent implements OnInit, OnDestroy {
  games: IGame[] = [];
  userGame: IGame;
  source: Subscription;
  _selectedGame: IGame;
  constructor(private _gameService: GameService, private _loginService: LoginService, private router: Router) { }

  ngOnInit() {
    this.source = timer(0, 5000).pipe(
      switchMap(() => this._gameService.GetGames())
    ).subscribe((result: CGMessage) => {
      if (result.status === true) {
        this.games = result.returnData[0] as IGame[];
      }
    });
  }

  ngOnDestroy() {
    if (!isNullOrUndefined(this.source)) {
      if (this.source.closed === false) {
        this.source.unsubscribe();
      }
      this.source = null;
    }
  }

  public saveGame() {
    this._gameService.SaveGame(this._selectedGame).subscribe(result => {
      if (result.status === true) {
        this._selectedGame = null;
      }
    });
  }

  public deleteGame(deleteGame: IGame) {
    this._gameService.DeleteGame(deleteGame).subscribe(() => { });
  }

  public joinGame(game: IGame) {
    const payLoad = { game: game, player: this._loginService.getPlayer() } as IPlayerGame;
    this._gameService.JoinGame(payLoad).subscribe(result => {
      if (result.status === true) {
        this.router.navigateByUrl('/game/' + game.id);
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
