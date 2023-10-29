import {Component} from '@angular/core';
import {Router} from '@angular/router';
import {MenuItem} from 'primeng/api/menuitem';
import {LoggingService} from '../services/logging.service';
import {LoginService} from '../services/login.service';
import {CGMessage} from '../shared/models/CGMessage';
import {IUser} from '../shared/models/player';
import {isNullOrUndefined} from '../shared/utils';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent
{
  COMPONENT_NAME: string = "LoginComponent";
  displayLogin: boolean = true;
  loginPlayer: IUser = {} as IUser;
  isLoginVisible: boolean = true;

  constructor(private _loginService: LoginService, 
    private router: Router, 
    private _loggingService: LoggingService) { }

  ngOnInit()
  {
    const METHOD_NAME: string = `${this.COMPONENT_NAME}.ngOnInit`;
    if (!isNullOrUndefined(localStorage.getItem('userName')))
    {
      this.loginPlayer.userName = localStorage.getItem('userName');
      this._loggingService.logDebug(`${METHOD_NAME}; LocalStorage UserName Found. Attempting to log in user ${this.loginPlayer.userName}`);
      this.login();
    }
  }

  login()
  {
    const METHOD_NAME: string = `${this.COMPONENT_NAME}.login`;
    this._loginService.Login(this.loginPlayer).subscribe((result: CGMessage) =>
    {
      if (result.status === true)
      {
        const p: IUser = result.returnData[0] as IUser;
        Promise.resolve(null).then(() => this._loginService.setUser(p)); // Called as promise to avoid ngChangeDetection error 

        localStorage.setItem('userName', p.userName);

        const menuItems = p.admin ? [
          {label: 'Home', icon: 'fa fa-fw fa-home', routerLink: 'home'},
          {label: 'Games', icon: 'fa fa-fw fa-gamepad', routerLink: 'games'},
          {label: 'Cards', icon: 'fa fa-fw fa-book', routerLink: 'cards'},
          {label: 'Logout', icon: 'fa fa-fw fa-sign-out', routerLink: 'logout'}
        ] as MenuItem[] :
          [
            {label: 'Home', icon: 'fa fa-fw fa-home', routerLink: 'home'},
            {label: 'Games', icon: 'fa fa-fw fa-gamepad', routerLink: 'games'},
            {label: 'Logout', icon: 'fa fa-fw fa-sign-out', routerLink: 'logout'}
          ] as MenuItem[];
        this._loginService.setMenuItems(menuItems);
        this.router.navigateByUrl('/home');
      }
      else
      {
        this._loggingService.logWarn(`${METHOD_NAME}; Error logging user in! Result: ${result}`);
      }
    });
  }
}
