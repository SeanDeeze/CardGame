import { Injectable } from '@angular/core';
import { IPlayer } from '../shared/models/player';
import { HttpHeaders } from '@angular/common/http/http';
import * as signalR from '@aspnet/signalr';
import { environment } from '../../environments/environment';
import { IGame } from '../shared/models/game';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  _players: IPlayer[] = [];
  _users: IPlayer[] = [];
  _games: IGame[] = [];
  _currentGame: IGame = {} as IGame;
  headers: HttpHeaders;
  private connection: signalR.HubConnection;
  constructor() { }


  public async connect(accessToken: string) {
    await this.start(accessToken);
  }

  public async start(accessToken: string) {
    if (!this.connection) {
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl(environment.signalR + 'api/gamehub', { accessTokenFactory: () => accessToken })
        .build();
    }

    if (this.connection && this.connection.state !== signalR.HubConnectionState.Connected) {
      this.connection.start().then(() => {
        if (this.connection && this.connection.state === signalR.HubConnectionState.Connected) {
          this.connection.invoke('SendLoggedInUsers').catch(function (err) {
            return console.error(err.toString());
          });
          this.connection.invoke('SendGames').catch(function (err) {
            return console.error(err.toString());
          });
          this.connection.on('ReceiveLoggedInUsers', (players: IPlayer[]) => {
            this._users = players;
          });
          this.connection.on('ReceiveGames', (games: IGame[]) => {
            this._games = games;
          });
          this.connection.on('ReceiveGameUsers', (players: IPlayer[]) => {
            this._players = players;
          });
          this.connection.on('ReceiveGameState', (game: IGame) => {
            this._currentGame = game;
          });
        }
      }).catch(err => {
        console.error(err);
      });
    }
  }

  public getEngineGames() {
    if (this.connection.state === signalR.HubConnectionState.Connected) {
      this.connection.invoke('SendGames').catch(function (err) {
        return console.error(err.toString());
      });
    }
  }

  public getGameState(gameId: number): Observable<any> {
    if (this.connection.state === signalR.HubConnectionState.Connected) {
      return of(this.connection.invoke('SendGameState', gameId).catch(function (err) {
        console.error(err.toString());
      }));
    }
    return of(false);
  }

  public addToGroup(groupId: number): Observable<any> {
    if (this.connection.state === signalR.HubConnectionState.Connected) {
      of(this.connection.invoke('AddToGroup', groupId));
    }
    return of(false);
  }

  public removeFromGroup(groupId: number): Observable<any> {
    if (this.connection.state === signalR.HubConnectionState.Connected) {
      return of(this.connection.invoke('RemoveFromGroup', groupId));
    }
  }

  public updatePlayerList(): void {
    if (this.connection.state === signalR.HubConnectionState.Connected) {
      this.connection.invoke('SendLoggedInUsers');
    }
  }

  public disconnect() {
    if (this.connection) {
      if (this.connection.state === signalR.HubConnectionState.Connected) {
        this.connection.stop();
      }
      this.connection = null;
    }
  }

  getUsers(): IPlayer[] {
    return this._users;
  }

  getPlayers(): IPlayer[] {
    return this._players;
  }

  getGames(): IGame[] {
    return this._games;
  }

  getCurrentGame(): IGame {
    return this._currentGame;
  }

  setCurrentGame(game: IGame): void {
    this._currentGame = game;
  }
}
