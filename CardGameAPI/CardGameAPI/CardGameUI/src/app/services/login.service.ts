import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Router} from '@angular/router';
import {MenuItem} from 'primeng/api';
import {Observable} from 'rxjs/Observable';
import {catchError, tap} from 'rxjs/operators';
import {environment} from '../../environments/environment';
import {CGMessage} from '../shared/models/CGMessage';
import {IPlayer} from '../shared/models/player';
import {isNullOrUndefined} from '../shared/utils';
import {LoggingService} from './logging.service';

@Injectable()
export class LoginService
{
  SERVICE_NAME: string = `LoginService`;
  headers: HttpHeaders;
  player: IPlayer = {} as IPlayer;
  menuItems: MenuItem[] = [];
  constructor(private _http: HttpClient, private _loggingService: LoggingService, private router: Router)
  {
    this.headers = new HttpHeaders()
      .set('Content-Type', 'application/json');
  }

  public Login(player: IPlayer): Observable<CGMessage>
  {
    return this._http.post<CGMessage>(environment.baseUrl + 'login/login', player, {headers: this.headers})
      .pipe(
        catchError(this._loggingService.handleError('login', []))
      );
  }

  public Logout(): Observable<CGMessage>
  {
    return this._http.post<CGMessage>(environment.baseUrl + 'login/logout', this.player, {headers: this.headers})
      .pipe(
        tap(() => {
          this.setPlayer({} as IPlayer); 
          localStorage.removeItem('userName');
          this.router.navigateByUrl('/login');}
        ),
        catchError(
          this._loggingService.handleError('login', [])
        )
      );
  }

  public GetLoggedInPlayers(): Observable<CGMessage>
  {
    return this._http.get<CGMessage>(environment.baseUrl + 'login/GetLoggedInPlayers', {headers: this.headers})
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
    return !isNullOrUndefined(this.player)
      && !isNullOrUndefined(this.player.id);
  }

  public getPlayer(): IPlayer
  {
    const METHOD_NAME: string = `${this.SERVICE_NAME}.getPlayer`;
    if (isNullOrUndefined(this.player))
    {
      this._loggingService.logWarn(`${METHOD_NAME}; Player object is unexpectedly NULL`);
    }
    return this.player;
  }

  public setPlayer(player: IPlayer)
  {
    this.player = player;
  }
}