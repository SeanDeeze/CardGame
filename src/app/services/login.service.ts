import { IPlayer } from '../shared/models/player';
import { environment } from '../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { catchError } from 'rxjs/operators';
import { CGMessage } from '../shared/models/CGMessage';
import { LoggingService } from './logging.service';
import { isNullOrUndefined } from 'util';
import * as signalR from '@aspnet/signalr';

@Injectable()
export class LoginService {
  player: IPlayer;
  headers: HttpHeaders;
  private connection: signalR.HubConnection;
  constructor(private _http: HttpClient, private _loggingService: LoggingService) {
    this.headers = new HttpHeaders()
      .set('Content-Type', 'application/json');
  }

  public Login(player: IPlayer): Observable<CGMessage> {
    return this._http.post<CGMessage>(environment.baseUrl + 'login/login', player, { headers: this.headers })
      .pipe(
        catchError(this._loggingService.handleError('login', []))
      );
  }

  public Logout(): Observable<CGMessage> {
    return this._http.post<CGMessage>(environment.baseUrl + 'login/logout', this.player, { headers: this.headers })
      .pipe(
        catchError(this._loggingService.handleError('login', []))
      );
  }

  public GetLoggedInPlayers(): Observable<CGMessage> {
    return this._http.get<CGMessage>(environment.baseUrl + 'login/GetLoggedInPlayers', { headers: this.headers })
      .pipe(
        catchError(this._loggingService.handleError('login', []))
      );
  }

  public KeepAlive(): Observable<CGMessage> {
    if (isNullOrUndefined(this.player)) { return new Observable<CGMessage>(); }
    return this._http.post<CGMessage>(environment.baseUrl + 'login/KeepAlive', this.player, { headers: this.headers })
      .pipe(
        catchError(this._loggingService.handleError('keepalive', []))
      );
  }

  public connect(accessToken) {
    if (!this.connection) {
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl('http://localhost:55891/gamehub', { accessTokenFactory: () => accessToken })
        .build();

      this.connection.on('receive', (user, msg) => {
        console.log('Received', user, msg);
      });

      this.connection.on('ReceiveLoggedInUsers', (players: IPlayer[]) => {
        console.log('Received Logged In Users', players.toString());
      });

      this.connection.start().then(() => {
        this.connection.invoke('SendLoggedInUsers').catch(function (err) {
          return console.error(err.toString());
        });
      }).catch(err => console.error(err));
    }
  }

  public disconnect() {
    if (this.connection) {
      this.connection.stop();
      this.connection = null;
    }
  }

  public isPlayerLoggedIn(): boolean {
    return !isNullOrUndefined(this.player) && !isNullOrUndefined(this.player.id);
  }

  public getPlayer(): IPlayer {
    return this.player;
  }

  public setPlayer(player: IPlayer) {
    this.player = player;
  }
}
