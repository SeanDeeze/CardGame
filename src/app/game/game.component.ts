import { Component, OnInit } from '@angular/core';
import { GameService } from '../services/game.service';
import { LoginService } from '../services/login.service';
import { SignalRService } from '../services/signal-r.service';
import { IGame, IPlayerGame, IDice } from '../shared/models/game';
import { IPlayer } from '../shared/models/player';
import { Router } from '@angular/router';
import { ICard } from '../shared/models/card';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {
  currentGame: IGame = {} as IGame;
  players: IPlayer[];
  dices: IDice[] = [{ diceValue: this.getRandomInt(1, 7) }, { diceValue: this.getRandomInt(1, 7) },
  { diceValue: this.getRandomInt(1, 7) }, { diceValue: this.getRandomInt(1, 7) },
  { diceValue: this.getRandomInt(1, 7) }, { diceValue: this.getRandomInt(1, 7) }];
  cardPiles: Array<Array<ICard>> = [[], [], [], [], [], []];
  digits: string[] = ['zero','one', 'two', 'three', 'four', 'five', 'six'];
  imageBase: string = environment.imageBase;

  constructor(private _gameService: GameService, private _loginService: LoginService, public _signalRService: SignalRService,
    private router: Router) { }

  ngOnInit() {
    this._gameService.IsPlayerInGame(this._loginService.getPlayer())
      .subscribe(response => {
        if (response.returnData.length === 0) {
          this.router.navigateByUrl('/games');
        } else {
          this._signalRService.setCurrentGame(response.returnData[0] as IGame);
          this.currentGame = this._signalRService.getCurrentGame();
          this.mapCardsFromGame();
        }
      });
  }

  public startGame(game: IGame) {
    this._gameService.StartGame(game).subscribe(result => {
      this._signalRService.getCurrentGame().active = true;
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
        this._signalRService.removeFromGroup(this._loginService.getPlayer().id);
        this._signalRService.setCurrentGame(null);
        this.router.navigateByUrl('/games');
      }
    });
  }

  public mapCardsFromGame() {
    if (this.currentGame != null) {
      for (let i = 0; i < this.currentGame.cards.length; i++) {
        this.cardPiles[i % 6].push(this.currentGame.cards[i]);
      }
    }
  }

  public rollDemBones() {
    this.dices = this.dices.map((d) => {
      return d.diceValue = this.getRandomInt(1, 7);
    });
  }

  public getRandomInt(min, max) {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min)) + min; // The maximum is exclusive and the minimum is inclusive
  }
}
