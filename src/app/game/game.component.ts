import { Component, OnInit } from '@angular/core';
import { GameService } from '../services/game.service';
import { LoginService } from '../services/login.service';
import { SignalRService } from '../services/signal-r.service';
import { IGame, IPlayerGame, IDice } from '../shared/models/game';
import { IPlayer } from '../shared/models/player';
import { Router } from '@angular/router';
import { ICard } from '../shared/models/card';
import { environment } from '../../environments/environment';
import { timer, Subscription } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { CGMessage } from '../shared/models/CGMessage';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {
  source: Subscription;
  players: IPlayer[];
  game: IGame;
  dices: IDice[] = [{ diceValue: this.getRandomInt(1, 7) }, { diceValue: this.getRandomInt(1, 7) },
  { diceValue: this.getRandomInt(1, 7) }, { diceValue: this.getRandomInt(1, 7) },
  { diceValue: this.getRandomInt(1, 7) }, { diceValue: this.getRandomInt(1, 7) }];
  cardPiles: Array<Array<ICard>> = [[], [], [], [], [], []];
  digits: string[] = ['zero', 'one', 'two', 'three', 'four', 'five', 'six'];
  imageBase: string = environment.imageBase;

  constructor(private _gameService: GameService, private _loginService: LoginService, public _signalRService: SignalRService,
    private router: Router) { }

  ngOnInit() {
    this._gameService.IsPlayerInGame(this._loginService.getPlayer())
      .subscribe(response => {
        if (response.returnData.length === 0) {
          this.router.navigateByUrl('/games');
        } else {
          this.source = timer(0, 2000).pipe(
            switchMap(() => this._gameService.GetGameState(1))
          ).subscribe((result: CGMessage) => {
            if (result.status === true) {
              this.game = result.returnData[0] as IGame;
            }
          });
        }
      });
  }

  public startGame(game: IGame) {
    this._gameService.StartGame(game).subscribe(result => {
      this._signalRService.getGameState(game.id);
    });
  }

  public endGame(game: IGame) {
    this._gameService.EndGame(game).subscribe(() => {
      this._signalRService.getCurrentGame().active = false;
      this.router.navigateByUrl('/games');
    });
  }

  public leaveGame(game: IGame) {
    const payLoad = { game: game, player: this._loginService.getPlayer() } as IPlayerGame;
    this._gameService.LeaveGame(payLoad).subscribe(result => {
      if (result.status === true) {
        this._signalRService.removeFromGroup(game.id).subscribe(() => {
          this._signalRService.setCurrentGame(null);
          this.router.navigateByUrl('/games');
        });
      }
    });
  }

  public mapCardsFromGame() {
    if (this._signalRService.getCurrentGame()) {
      for (let i = 0; i < this._signalRService.getCurrentGame().cards.length; i++) {
        this.cardPiles[i % 6].push(this._signalRService.getCurrentGame().cards[i]);
      }
    }
  }

  public rollDemBones() {
    this.dices.forEach(dice => {
      return dice.diceValue = this.getRandomInt(1, 7);
    });
  }

  public getRandomInt(min: number, max: number) {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min)) + min; // The maximum is exclusive and the minimum is inclusive
  }
}
