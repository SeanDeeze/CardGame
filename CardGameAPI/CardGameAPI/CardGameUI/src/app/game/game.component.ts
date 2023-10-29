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
  digits: string[] = ['zero', 'one', 'two', 'three', 'four', 'five', 'six'];
  imageBase: string = environment.imageBase;

  constructor(private _gameService: GameService, private _loginService: LoginService, private router: Router,
    private route: ActivatedRoute, private _loggingService: LoggingService) { }

  ngOnInit()
  {
    this.game = {} as IGame;
    this.currentUser = this._loginService.getUser();
    this.route.params.subscribe((params: Params) =>
    {
      this.game.id = params['id'];
      this.source = timer(0, 5000)
        .pipe(switchMap(() => this._gameService.GetGameState(this.game)))
        .subscribe((result: CGMessage) =>
        {
          this.handleGameState(result);
        });
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
      this.handleGameState(result);
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
    const payLoad = {game: game, player: this._loginService.getUser()} as IPlayerGame;
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
