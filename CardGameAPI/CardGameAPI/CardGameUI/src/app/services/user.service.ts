import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { CGMessage } from '../shared/models/CGMessage';
import { IPlayer } from '../shared/models/player';
import { LoggingService } from './logging.service';

@Injectable({
  providedIn: 'root'
})
export class UserService
{
  headers: HttpHeaders;
  player: IPlayer = {} as IPlayer;
  constructor(private _http: HttpClient, private _loggingService: LoggingService)
  {
    this.headers = new HttpHeaders()
      .set('Content-Type', 'application/json');
  }

  public GetUsers(): Observable<CGMessage>
  {
    return this._http.get<CGMessage>(environment.baseUrl + 'user/GetUsers', { headers: this.headers })
      .pipe(
        catchError(this._loggingService.handleError('users', []))
      );
  }
}
