import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { catchError, retry } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { CGMessage } from '../shared/models/CGMessage';
import { IGame, IPlayerGame } from '../shared/models/game';
import { LoggingService } from './logging.service';

@Injectable({
  providedIn: 'root'
})
export class GameService
{
  headers: HttpHeaders;
  private _game: IGame;
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

  public GetGameState(gameId: number): Observable<CGMessage>
  {
    return this._http.post<CGMessage>(environment.baseUrl + 'game/GetGameState', gameId, { headers: this.headers })
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

  public DeleteGame(selectedGame: IGame): Observable<CGMessage>
  {
    return this._http.post<CGMessage>(environment.baseUrl + 'game/deletegame', selectedGame, { headers: this.headers })
      .pipe(
        retry(3),
        catchError(this._loggingService.handleError('savegame', []))
      );
  }

  public JoinGame(payLoad: IPlayerGame): Observable<CGMessage>
  {
    this._game = payLoad.game;
    return this._http.post<CGMessage>(environment.baseUrl + 'game/joingame', payLoad, { headers: this.headers })
      .pipe(
        retry(3),
        catchError(this._loggingService.handleError('savegame', []))
      );
  }

  public LeaveGame(payLoad: IPlayerGame): Observable<CGMessage>
  {
    this._game = null;
    return this._http.post<CGMessage>(environment.baseUrl + 'game/leavegame', payLoad, { headers: this.headers })
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

  public getGame(): IGame
  {
    return this._game;
  }
}
