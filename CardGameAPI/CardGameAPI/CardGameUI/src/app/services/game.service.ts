import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs/Observable';
import {catchError, retry} from 'rxjs/operators';
import {environment} from '../../environments/environment';
import {CGMessage} from '../shared/models/CGMessage';
import {IGame, IPlayerGame} from '../shared/models/game';
import {LoggingService} from './logging.service';

@Injectable({
  providedIn: 'root'
})
export class GameService
{
  headers: HttpHeaders;
  public game: IGame;
  constructor(private _http: HttpClient, private _loggingService: LoggingService)
  {
    this.headers = new HttpHeaders()
      .set('Content-Type', 'application/json');
  }

  public GetGames(): Observable<CGMessage>
  {
    return this._http.get<CGMessage>(environment.baseUrl + 'game/getgames', { headers: this.headers })
      .pipe(
        retry(3),
        catchError(this._loggingService.handleError('getgames', []))
      );
  }

  public GetGameState(game: IGame): Observable<CGMessage>
  {
    return this._http.post<CGMessage>(environment.baseUrl + 'game/GetGameState', game, { headers: this.headers })
      .pipe(
        retry(3),
        catchError(this._loggingService.handleError('getgamestate', []))
      );
  }

  public SaveGame(selectedGame: IGame): Observable<CGMessage>
  {
    return this._http.put<CGMessage>(environment.baseUrl + 'game/savegame', selectedGame, { headers: this.headers })
      .pipe(
        retry(3),
        catchError(this._loggingService.handleError('savegame', []))
      );
  }

  public DeleteGame(deleteGame: IPlayerGame): Observable<CGMessage>
  {
    return this._http.post<CGMessage>(environment.baseUrl + 'game/deletegame', deleteGame, { headers: this.headers })
      .pipe(
        retry(3),
        catchError(this._loggingService.handleError('DeleteGame', []))
      );
  }

  public JoinGame(playerGame: IPlayerGame): Observable<CGMessage>
  {
    return this._http.post<CGMessage>(environment.baseUrl + 'game/joingame', playerGame, { headers: this.headers })
      .pipe(
        retry(3),
        catchError(this._loggingService.handleError('JoinGame', []))
      );
  }

  public LeaveGame(playerGame: IPlayerGame): Observable<CGMessage>
  {
    return this._http.post<CGMessage>(environment.baseUrl + 'game/leavegame', playerGame, { headers: this.headers })
      .pipe(
        retry(3),
        catchError(this._loggingService.handleError('savegame', []))
      );
  }

  public StartGame(selectedGame: IGame): Observable<CGMessage>
  {
    return this._http.post<CGMessage>(environment.baseUrl + 'game/StartGame', selectedGame, { headers: this.headers })
      .pipe(
        retry(3),
        catchError(this._loggingService.handleError('savegame', []))
      );
  }

  public EndGame(selectedGame: IGame): Observable<CGMessage>
  {
    return this._http.post<CGMessage>(environment.baseUrl + 'game/EndGame', selectedGame, { headers: this.headers })
      .pipe(
        retry(3),
        catchError(this._loggingService.handleError('endgame', []))
      );
  }

  public GameState(selectedGame: IGame): Observable<CGMessage>
  {
    return this._http.post<CGMessage>(environment.baseUrl + 'game/GameState', selectedGame, { headers: this.headers })
      .pipe(
        retry(3),
        catchError(this._loggingService.handleError('savegame', []))
      );
  }
}
