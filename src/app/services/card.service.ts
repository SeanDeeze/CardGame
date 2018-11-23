import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { LoggingService } from './logging.service';
import { ICard } from '../shared/models/card';
import { Observable } from 'rxjs/Observable';
import { CGMessage } from '../shared/models/CGMessage';
import { environment } from '../../environments/environment';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CardService {
  headers: HttpHeaders;
  cards: ICard[] = [];
  constructor(private _http: HttpClient, private _loggingService: LoggingService) {
    this.headers = new HttpHeaders()
      .set('Content-Type', 'application/json');
  }
  public GetCards(): Observable<CGMessage> {
    return this._http.post<CGMessage>(environment.baseUrl + 'card/getcards', { headers: this.headers })
    .pipe(
      catchError(this._loggingService.handleError('getcards', []))
    );
  }

}
