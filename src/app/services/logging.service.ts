import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { CGMessage } from '../shared/models/CGMessage';

@Injectable({
  providedIn: 'root'
})
export class LoggingService {

  constructor() { }

  public handleError<T>(operation: string, result?: T) {
    return (error: any): Observable<CGMessage> => {
      console.error(error); // log to console
      return;
    };
  }
}
