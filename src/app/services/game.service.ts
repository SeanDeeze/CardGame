import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { CGMessage } from '../shared/models/CGMessage';
import { environment } from '../../environments/environment';
import { LoggingService } from './logging.service';
import { catchError, retry } from 'rxjs/operators';
import { IGame, IPlayerGame } from '../shared/models/game';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  headers: HttpHeaders;
  private _game: IGame;
  constructor(private _http: HttpClient, private _loggingService: LoggingService) {
    this.headers = new HttpHeaders()
      .set('Content-Type', 'application/json');
  }

  public GetGames(): Observable<CGMessage> {
    return this._http.post<CGMessage>(environment.baseUrl + 'game/getgames', { headers: this.headers })
      .pipe(
        retry(3),
        catchError(this._loggingService.handleError('getgames', []))
      );
  }

  public SaveGame(selectedGame: IGame): Observable<CGMessage> {
    return this._http.put<CGMessage>(environment.baseUrl + 'game/savegame', selectedGame, { headers: this.headers })
      .pipe(
        retry(3),
        catchError(this._loggingService.handleError('savegame', []))
      );
  }

  public DeleteGame(selectedGame: IGame): Observable<CGMessage> {
    return this._http.post<CGMessage>(environment.baseUrl + 'game/deletegame', selectedGame, { headers: this.headers })
      .pipe(
        retry(3),
        catchError(this._loggingService.handleError('savegame', []))
      );
  }

  public JoinGame(payLoad: IPlayerGame): Observable<CGMessage> {
    this._game = payLoad.game;
    return this._http.post<CGMessage>(environment.baseUrl + 'game/joingame', payLoad, { headers: this.headers })
      .pipe(
        retry(3),
        catchError(this._loggingService.handleError('savegame', []))
      );
  }

  public LeaveGame(payLoad: IPlayerGame): Observable<CGMessage> {
    this._game = null;
    return this._http.post<CGMessage>(environment.baseUrl + 'game/leavegame', payLoad, { headers: this.headers })
      .pipe(
        retry(3),
        catchError(this._loggingService.handleError('savegame', []))
      );
  }
}
