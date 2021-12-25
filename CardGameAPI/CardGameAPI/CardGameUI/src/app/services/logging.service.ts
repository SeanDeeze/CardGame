import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { CGMessage } from '../shared/models/CGMessage';

@Injectable({
  providedIn: 'root'
})
export class LoggingService
{

  constructor() { }

  public logWarn(message: string, data: any = null)
  {
    console.warn(`${ new Date() } - Warning: ${ message }. Relevent Data: ${ data }`);
  }

  public logInfo(message: string, data: any = null)
  {
    console.info(`${ new Date() } - Warning: ${ message }. Relevent Data: ${ data }`);
  }

  public logDebug(message: string, data: any = null)
  {
    console.debug(`${ new Date() } - Warning: ${ message }. Relevent Data: ${ data }`);
  }

  public logError(message: string, data: any = null)
  {
    console.error(`${ new Date() } - Warning: ${ message }. Relevent Data: ${ data }`);
  }

  public handleError<T>(operation: string, result?: T)
  {
    return (error: any): Observable<CGMessage> =>
    {
      console.error(`${ new Date() } - Operation: ${ operation } Error: ${ error }.`); // log to console
      return;
    };
  }
}
