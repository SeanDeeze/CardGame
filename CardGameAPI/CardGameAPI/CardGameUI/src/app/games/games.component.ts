import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription, timer } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { GameService } from '../services/game.service';
import { LoggingService } from '../services/logging.service';
import { LoginService } from '../services/login.service';
import { CGMessage } from '../shared/models/CGMessage';
import { IGame, IPlayerGame } from '../shared/models/game';
import { isNullOrUndefined } from '../shared/utils';

@Component({
  selector: 'app-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.css']
})
export class GamesComponent implements OnInit, OnDestroy
{
  games: IGame[] = [];
  userGame: IGame;
  source: Subscription;
  _selectedGame: IGame;
  constructor(private _gameService: GameService, private _loginService: LoginService, private _router: Router, private _loggingService: LoggingService) { }

  ngOnInit()
  {
    this.source = timer(0, 5000).pipe(
      switchMap(() => this._gameService.GetGames())
    )
      .subscribe((result: CGMessage) =>
      {
        if (result.status === true)
        {
          this.games = result.returnData[0] as IGame[];
        }
      });
  }

  ngOnDestroy()
  {
    if (!isNullOrUndefined(this.source))
    {
      if (this.source.closed === false)
      {
        this.source.unsubscribe();
      }
      this.source = null;
    }
  }

  public saveGame()
  {
    this._gameService.SaveGame(this._selectedGame).subscribe(result =>
    {
      if (result.status === true)
      {
        this._selectedGame = null;
      }
    });
  }

  public deleteGame(deleteGame: IGame)
  {
    this._gameService.DeleteGame(deleteGame).subscribe(() => { });
  }

  public joinGame(game: IGame)
  {
    const payLoad: IPlayerGame = { game: game, player: this._loginService.getPlayer() } as IPlayerGame;
    this._gameService.JoinGame(payLoad).subscribe(result =>
    {
      if (result.status === true)
      {
        this._router.navigateByUrl('/game/' + game.id);
      }
      else
      {
        this._loggingService.logWarn(`Attempt to join game was unsuccessful`, result.message);
      }
    });
  }

  addGame()
  {
    this._selectedGame = {} as IGame;
  }

  unselectGame()
  {
    this._selectedGame = null;
  }

}
