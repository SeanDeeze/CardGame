import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription, timer } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { GameService } from '../services/game.service';
import { LoginService } from '../services/login.service';
import { ICard } from '../shared/models/card';
import { CGMessage } from '../shared/models/CGMessage';
import { IDice, IGame, IPlayerGame } from '../shared/models/game';
import { IPlayer } from '../shared/models/player';
import { isNullOrUndefined } from '../shared/utils';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit, OnDestroy
{
  source: Subscription;
  players: IPlayer[];
  game: IGame;
  gameId: number;
  dices: IDice[] = [{ diceValue: this.getRandomInt(1, 7) }, { diceValue: this.getRandomInt(1, 7) },
  { diceValue: this.getRandomInt(1, 7) }, { diceValue: this.getRandomInt(1, 7) },
  { diceValue: this.getRandomInt(1, 7) }, { diceValue: this.getRandomInt(1, 7) }];
  cardPiles: Array<Array<ICard>> = [[], [], [], [], [], []];
  digits: string[] = ['zero', 'one', 'two', 'three', 'four', 'five', 'six'];
  imageBase: string = environment.imageBase;

  constructor(private _gameService: GameService, private _loginService: LoginService, private router: Router,
    private route: ActivatedRoute) { }

  ngOnInit()
  {
    this.route.params.subscribe(params =>
    {
      this.gameId = params['id'];
      this.source = timer(0, 5000).pipe(
        switchMap(() => this._gameService.GetGameState(this.gameId))
      ).subscribe((result: CGMessage) =>
      {
        if (result.status === true)
        {
          this.game = result.returnData[0] as IGame;
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

  public startGame(game: IGame)
  {
    this._gameService.StartGame(game).subscribe(() => { });
  }

  public endGame(game: IGame)
  {
    this._gameService.EndGame(game).subscribe(() =>
    {
      this.router.navigateByUrl('/games');
    });
  }

  public leaveGame(game: IGame)
  {
    const payLoad = { game: game, player: this._loginService.getPlayer() } as IPlayerGame;
    this._gameService.LeaveGame(payLoad).subscribe(result =>
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
