import {Injectable} from '@angular/core';
import {Observable} from 'rxjs/Observable';
import {CGMessage} from '../shared/models/CGMessage';

@Injectable({
  providedIn: 'root'
})
export class LoggingService
{

  constructor() { }

  public logWarn(message: string)
  {
    console.warn(`${ new Date() } - Warning: ${ message }`);
  }

  public logInfo(message: string)
  {
    console.info(`${ new Date() } - Info: ${ message }`);
  }

  public logDebug(message: string)
  {
    console.debug(`${ new Date() } - Debug: ${ message }`);
  }

  public logError(message: string)
  {
    console.error(`${ new Date() } - Error: ${ message }`);
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
