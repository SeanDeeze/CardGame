import { Injectable } from '@angular/core';
import { IPlayer } from '../shared/models/player';
import { HttpHeaders } from '@angular/common/http/http';
import { Subscription } from 'rxjs/internal/Subscription';
import * as signalR from '@aspnet/signalr';
import { interval } from 'rxjs/observable/interval';
import { environment } from '../../environments/environment';
import { IGame } from '../shared/models/game';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  _players: IPlayer[] = [];
  _games: IGame[] = [];
  headers: HttpHeaders;
  subscription: Subscription;
  private connection: signalR.HubConnection;
  constructor() { }


  public connect(accessToken) {
    if (!this.connection) {
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl(environment.signalR + 'api/gamehub', { accessTokenFactory: () => accessToken })
        .build();

      this.connection.start().then(() => {
        const source = interval(1000);
        this.subscription = source.subscribe(val => {
          this.connection.invoke('SendLoggedInUsers').catch(function (err) {
            return console.error(err.toString());
          });
          this.connection.invoke('SendGames').catch(function (err) {
            return console.error(err.toString());
          });
        });

        this.connection.on('ReceiveLoggedInUsers', (players: IPlayer[]) => {
          this._players = players;
        });

        this.connection.on('ReceiveGames', (games: IGame[]) => {
          this._games = games;
        });
      }).catch(err => {
        if (!environment.production) {
          console.error(err);
        }
      });
    }
  }

  public disconnect() {
    if (this.connection) {
      this.connection.stop();
      this.connection = null;
    }
  }

  getPlayers(): IPlayer[] {
    return this._players;
  }

  getGames(): IGame[] {
    return this._games;
  }
}
