import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {LoggingService} from './services/logging.service';
import {LoginService} from './services/login.service';
import {isNullOrUndefined} from './shared/utils';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit
{
  COMPONENT_NAME: string = "LoginComponent";
  displayLogin = true;

  constructor(public _loginService: LoginService, 
    private router: Router,
    private _loggingService: LoggingService) { }

  ngOnInit()
  {
    const METHOD_NAME: string = `${this.COMPONENT_NAME}.ngOnInit`;
    if (!this._loginService.isPlayerLoggedIn())
    {
      const removeLocalStorageValues: boolean = false;
      this._loginService.Logout(removeLocalStorageValues).subscribe(() =>{});
    }
    else if (isNullOrUndefined(this._loginService.getUser()))
    {
      this._loggingService.logDebug(`${METHOD_NAME}; User is null or undefined. Redirecting to Login Page now`);
      this.router.navigateByUrl('/login');
    }
  }
}
