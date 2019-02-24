import { Component, OnInit } from '@angular/core';
import { GameService } from '../services/game.service';
import { LoginService } from '../services/login.service';
import { SignalRService } from '../services/signal-r.service';
import { IGame, IPlayerGame } from '../shared/models/game';
import { IPlayer } from '../shared/models/player';
import { Router } from '@angular/router';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {
  currentGame: IGame;
  players: IPlayer[];
  constructor(private _gameService: GameService, private _loginService: LoginService, public _signalRService: SignalRService,
    private router: Router) { }

  ngOnInit() {
    this.currentGame = this._gameService.getGame();
  }

  public startGame(game: IGame) {
    this._gameService.StartGame(game);
  }

  public leaveGame(game: IGame) {
    const payLoad = { game: game, player: this._loginService.getPlayer() } as IPlayerGame;
    this._gameService.LeaveGame(payLoad).subscribe(result => {
      if (result.status === true) {
        this._signalRService.removeFromGroup(this._loginService.getPlayer().id);
        this.currentGame = null;
        this.router.navigateByUrl('/games');
      }
    });
  }

}
