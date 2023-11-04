import {HttpClient} from '@angular/common/http';
import {Component, OnDestroy, OnInit} from '@angular/core';
import {ActivatedRoute, Params, Router} from '@angular/router';
import {Subscription, timer} from 'rxjs';
import {switchMap} from 'rxjs/operators';
import {environment} from '../../environments/environment';
import {GameService} from '../services/game.service';
import {LoggingService} from '../services/logging.service';
import {LoginService} from '../services/login.service';
import {CGMessage} from '../shared/models/CGMessage';
import {IGame, IGameState, IPlayerGame} from '../shared/models/game';
import {IGamePlayer, IUser} from '../shared/models/player';
import {isNullOrUndefined} from '../shared/utils';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit, OnDestroy
{
  COMPONENT_NAME: string = 'GameComponent';
  METHOD_NAME: string = '';

  source: Subscription;
  currentUser: IUser;
  currentPlayer: IGamePlayer;
  activePlayer: IGamePlayer;
  game: IGame;
  gameId: number;
  gameState: IGameState;
  imageBase: string = environment.imageBase;

  constructor(private _gameService: GameService, 
    private _loginService: LoginService, 
    private router: Router,
    private route: ActivatedRoute, 
    private _loggingService: LoggingService,
    private _http: HttpClient) { }

  ngOnInit()
  {
    this.game = {} as IGame;
    this.currentUser = this._loginService.getUser();
    this.route.params.subscribe((params: Params) =>
    {
      this.game.id = params['id'];

      if(environment.testData)
      {
        this._loggingService.logDebug(`${this.METHOD_NAME}; Loading GameState Data from file!`);

        this.source = timer(0, 2500)
        .pipe(switchMap(() => this._http.get("assets/testData/gameState.json")))
        .subscribe((response: CGMessage) => {
          this.handleGameState(response);
        });
      }
      else 
      {
        this.source = timer(0, 2500)
        .pipe(switchMap(() => this._gameService.GetGameState(this.game)))
        .subscribe((response: CGMessage) => {
          this.handleGameState(response);
        });
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

  public handleGameState(gameStateResponse: CGMessage)
  {
    this.METHOD_NAME = `${this.COMPONENT_NAME}.handleGameState`;
    if (gameStateResponse.status === true)
    {
      this.gameState = gameStateResponse.returnData[0] as IGameState;
      this.currentPlayer = this.gameState.gamePlayers.find(gp => gp.player.id === this.currentUser.id);



      if (!isNullOrUndefined(this.gameState.currentGamePlayerId))
      {
        this.activePlayer = this.gameState.gamePlayers.find(gp => gp.player.id === this.gameState.currentGamePlayerId);
      } 
    }
    else
    {
      this._loggingService.logWarn(`${this.METHOD_NAME}; The serive call resulted in a non-successful response. 
        This user screen is likely does not reflect current game status. See Logs for additional details; Result: ${gameStateResponse}`);
    }
  }

  public startGame(game: IGame)
  {
    this._gameService.StartGame(game).subscribe((result: CGMessage) => 
    {
      this._loggingService.logWarn(`${this.METHOD_NAME}; Game Start Command Invoked. 
        Result: ${result.status}, Message: ${result.message}`);
    });
  }

  public endGame(game: IGame)
  {
    this._gameService.EndGame(game).subscribe((result: CGMessage) =>
    {
      if (result.status === true)
      {
        this.router.navigateByUrl('/games');
      }
      else
      {
        this._loggingService.logWarn(`${this.METHOD_NAME}; The serive call resulted in a non-successful response. 
        The Game was not able to be ended; Result: ${result}`);
      }

    });
  }

  public leaveGame(game: IGame)
  {
    const payLoad = {gameID: game.id, playerID: this._loginService.getUser().id} as IPlayerGame;
    this._gameService.LeaveGame(payLoad).subscribe((result: CGMessage) =>
    {
      if (result.status === true)
      {
        this.router.navigateByUrl('/games');
      }
      else
      {
        this._loggingService.logWarn(`${this.METHOD_NAME}; The serive call resulted in a non-successful response. 
        Could Not remove the current user from the game; Result: ${result}`);
      }
    });
  }

  public rollDemBones()
  {
    this.game.engine.gamePlayers.forEach(() =>
    {
      return this.getRandomInt(1, 7);
    });
  }

  public getRandomInt(min: number, max: number)
  {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min)) + min; // The maximum is exclusive and the minimum is inclusive
  }
}
