import {Component, OnDestroy, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {Subscription, timer} from 'rxjs';
import {switchMap} from 'rxjs/operators';
import {GameService} from '../services/game.service';
import {LoggingService} from '../services/logging.service';
import {LoginService} from '../services/login.service';
import {CGMessage} from '../shared/models/CGMessage';
import {IGame, IPlayerGame} from '../shared/models/game';
import {isNullOrUndefined} from '../shared/utils';

@Component({
  selector: 'app-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.css']
})
export class GamesComponent implements OnInit, OnDestroy
{
  COMPONENT_NAME: string = 'GamesComponent';
  METHOD_NAME: string = '';

  games: IGame[] = [];
  userGame: IGame;
  source: Subscription;
  _selectedGame: IGame;

  refreshTime: number = 3500;

  constructor(private _gameService: GameService, 
    private _loginService: LoginService,
    private _router: Router, 
    private _loggingService: LoggingService) { }

  ngOnInit()
  {
    this.METHOD_NAME = `${this.COMPONENT_NAME}.OnInit`;
    this.getGamesSubscription();
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

  public getGamesSubscription()
  {
    this.METHOD_NAME = `${this.COMPONENT_NAME}.getGamesSubscription`;
    this.source = timer(0, this.refreshTime)
    .pipe(
      switchMap(() => this._gameService.GetGames()))
    .subscribe((result: CGMessage) =>
    {
      if (result.status === true)
      {
        this.games = result.returnData[0] as IGame[];
      }
      else 
      {
        this._loggingService.logWarn(`${this.METHOD_NAME}; The service call did not return a value 
          indicating success; Result: ${result}`);
      }
    });

  }

  public saveGame()
  {
    this.METHOD_NAME = `${this.COMPONENT_NAME}.saveGame`;
    this._gameService.SaveGame(this._selectedGame).subscribe((result: CGMessage) =>
    {
      if (result.status === true)
      {
        this._selectedGame = null;
      }
      else 
      {
        this._loggingService.logWarn(`${this.METHOD_NAME}; The service call did not return a value 
            indicating success; Result: ${result}`);
      }
    });
  }

  public deleteGame(deleteGame: IGame)
  {
    this.METHOD_NAME = `${this.COMPONENT_NAME}.deleteGame`;
    this._gameService.DeleteGame(deleteGame).subscribe((result: CGMessage) => 
    {
      this._loggingService.logError(`${this.METHOD_NAME}; Error Status returned when attempting to delete game; Result: ${result}`);
     });
  }

  public joinGame(gameID: string)
  {
    const playerGame: IPlayerGame = {gameID: gameID, playerID: this._loginService.getUser().id} as IPlayerGame;

    this._gameService.JoinGame(playerGame).subscribe(result =>
    {
      if (result.status === true)
      {
        this._router.navigateByUrl(`/game/${gameID}`);
      }
      else
      {
        this._loggingService.logWarn(`Attempt to join game was unsuccessful`);
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
