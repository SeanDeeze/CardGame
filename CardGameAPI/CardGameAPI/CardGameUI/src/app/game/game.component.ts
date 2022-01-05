import {Component, OnDestroy, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {Subscription, timer} from 'rxjs';
import {switchMap} from 'rxjs/operators';
import {environment} from '../../environments/environment';
import {GameService} from '../services/game.service';
import {LoggingService} from '../services/logging.service';
import {LoginService} from '../services/login.service';
import {ICard} from '../shared/models/card';
import {CGMessage} from '../shared/models/CGMessage';
import {IDice, IGame, IGameState, IPlayerGame} from '../shared/models/game';
import {IPlayer} from '../shared/models/player';
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
  players: IPlayer[];
  game: IGame;
  gameId: number;
  gameState: IGameState;
  dices: IDice[] = [{diceValue: this.getRandomInt(1, 7)}, {diceValue: this.getRandomInt(1, 7)},
  {diceValue: this.getRandomInt(1, 7)}, {diceValue: this.getRandomInt(1, 7)},
  {diceValue: this.getRandomInt(1, 7)}, {diceValue: this.getRandomInt(1, 7)}];
  cardPiles: Array<Array<ICard>> = [[], [], [], [], [], []];
  digits: string[] = ['zero', 'one', 'two', 'three', 'four', 'five', 'six'];
  imageBase: string = environment.imageBase;

  constructor(private _gameService: GameService, private _loginService: LoginService, private router: Router,
    private route: ActivatedRoute, private _loggingService: LoggingService) { }

  ngOnInit()
  {
    this.game = {} as IGame;
    this.route.params.subscribe(params =>
    {
      this.game.id = params['id'];
      this.source = timer(0, 5000).pipe(
        switchMap(() => this._gameService.GetGameState(this.game))
      ).subscribe((result: CGMessage) =>
      {
        if (result.status === true)
        {
          this.gameState = result.returnData[0] as IGameState;
        }
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

  public getGameState()
  {
    this.METHOD_NAME = `${this.COMPONENT_NAME}.getGameState`;
    this._gameService.GetGameState(this.game).subscribe((result: CGMessage) => 
    {
      if (result.status === true)
      {

      }
      else
      {
        this._loggingService.logWarn(`${this.METHOD_NAME}; The serive call resulted in a non-successful response. 
        This user screen is likely does not reflect current game status. See Logs for additional details; Result: ${result}`);
      }
    });
  }

  public startGame(game: IGame)
  {
    this._gameService.StartGame(game).subscribe((result: CGMessage) => 
    {
      if (result.status === true)
      {
        this.getGameState();
      }
    });
  }

  public endGame(game: IGame)
  {
    this._gameService.EndGame(game).subscribe((result: CGMessage) =>
    {
      this.router.navigateByUrl('/games');
    });
  }

  public leaveGame(game: IGame)
  {
    const payLoad = {game: game, player: this._loginService.getPlayer()} as IPlayerGame;
    this._gameService.LeaveGame(payLoad).subscribe((result: CGMessage) =>
    {
      if (result.status === true)
      {
        this.router.navigateByUrl('/games');
      }
    });
  }

  public mapCardsFromGame()
  {
    for (let i = 0; i < this.game.cards.length; i++)
    {
      this.cardPiles[i % 6].push(this.game.cards[i]);
    }
  }

  public rollDemBones()
  {
    this.dices.forEach(dice =>
    {
      return dice.diceValue = this.getRandomInt(1, 7);
    });
  }

  public getRandomInt(min: number, max: number)
  {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min)) + min; // The maximum is exclusive and the minimum is inclusive
  }
}
