import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs/Observable';
import {catchError} from 'rxjs/operators';
import {environment} from '../../environments/environment';
import {CGMessage} from '../shared/models/CGMessage';
import {ICard, ICardRole} from '../shared/models/card';
import {LoggingService} from './logging.service';

@Injectable({
  providedIn: 'root'
})
export class CardService
{
  headers: HttpHeaders;
  constructor(private _http: HttpClient, private _loggingService: LoggingService)
  {
    this.headers = new HttpHeaders()
      .set('Content-Type', 'application/json');
  }
  public GetCards(): Observable<CGMessage>
  {
    return this._http.get<CGMessage>(environment.baseUrl + 'card/getcards', {headers: this.headers})
      .pipe(
        catchError(this._loggingService.handleError('getcards', []))
      );
  }

  public GetCardRoles(): Observable<CGMessage>
  {
    return this._http.get<CGMessage>(environment.baseUrl + 'card/getcardroles', {headers: this.headers})
      .pipe(
        catchError(this._loggingService.handleError('getcardroles', []))
      );
  }

  public SaveCard(card: ICard): Observable<CGMessage>
  {
    return this._http.put<CGMessage>(environment.baseUrl + 'card/savecard', card, {headers: this.headers})
      .pipe(
        catchError(this._loggingService.handleError('savecard', []))
      );
  }

  public SaveCardRole(cardRole: ICardRole): Observable<CGMessage>
  {
    return this._http.put<CGMessage>(environment.baseUrl + 'card/savecardrole', cardRole, {headers: this.headers})
      .pipe(
        catchError(this._loggingService.handleError('savecardrole', []))
      );
  }

  public DeleteCard(card: ICard): Observable<CGMessage>
  {
    return this._http.post<CGMessage>(environment.baseUrl + 'card/deletecard', card, {headers: this.headers})
      .pipe(
        catchError(this._loggingService.handleError('deletecard', []))
      );
  }

  public DeleteCardRole(cardRole: ICardRole): Observable<CGMessage>
  {
    return this._http.post<CGMessage>(environment.baseUrl + 'card/deletecardrole', cardRole, {headers: this.headers})
      .pipe(
        catchError(this._loggingService.handleError('deletecardrole', []))
      );
  }

}
