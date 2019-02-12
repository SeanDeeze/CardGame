import { IPlayer } from '../shared/models/player';
import { environment } from '../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { catchError } from 'rxjs/operators';
import { CGMessage } from '../shared/models/CGMessage';
import { LoggingService } from './logging.service';
import { isNullOrUndefined } from 'util';

@Injectable()
export class LoginService {
  headers: HttpHeaders;
  player: IPlayer;
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
