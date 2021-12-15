import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { Observable } from 'rxjs/Observable';
import { catchError } from 'rxjs/operators';
import { isNullOrUndefined } from 'util';
import { environment } from '../../environments/environment';
import { CGMessage } from '../shared/models/CGMessage';
import { IPlayer } from '../shared/models/player';
import { LoggingService } from './logging.service';

@Injectable()
export class LoginService
{
  headers: HttpHeaders;
  player: IPlayer = {} as IPlayer;
  menuItems: MenuItem[] = [];
  constructor(private _http: HttpClient, private _loggingService: LoggingService)
  {
    this.headers = new HttpHeaders()
      .set('Content-Type', 'application/json');
  }

  public Login(player: IPlayer): Observable<CGMessage>
  {
    return this._http.post<CGMessage>(environment.baseUrl + 'login/login', player, { headers: this.headers })
      .pipe(
        catchError(this._loggingService.handleError('login', []))
      );
  }

  public Logout(): Observable<CGMessage>
  {
    return this._http.post<CGMessage>(environment.baseUrl + 'login/logout', this.player, { headers: this.headers })
      .pipe(
        catchError(
          this._loggingService.handleError('login', [])
        )
      );
  }

  public GetLoggedInPlayers(): Observable<CGMessage>
  {
    return this._http.get<CGMessage>(environment.baseUrl + 'login/GetLoggedInPlayers', { headers: this.headers })
      .pipe(
        catchError(this._loggingService.handleError('login', []))
      );
  }

  public getMenuItems(): MenuItem[]
  {
    return this.menuItems;
  }

  public setMenuItems(menuItems: MenuItem[]): MenuItem[]
  {
    this.menuItems = menuItems;
    return this.menuItems;
  }

  public isPlayerLoggedIn(): boolean
  {
    return !isNullOrUndefined(this.player) && !isNullOrUndefined(this.player.id);
  }

  public getPlayer(): IPlayer
  {
    return this.player;
  }

  public setPlayer(player: IPlayer)
  {
    this.player = player;
  }
}
