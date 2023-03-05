import {Component, OnDestroy, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {LoginService} from './services/login.service';
import {IUser} from './shared/models/player';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy
{
  displayLogin = true;

  constructor(public _loginService: LoginService, private router: Router) { }

  async ngOnInit()
  {
    if (!this._loginService.isPlayerLoggedIn())
    {
      this.logout();
    }
    else if (this._loginService.getUser() == null)
    {
      this.router.navigateByUrl('/login');
    }
  }

  ngOnDestroy()
  {
    this.logout();
  }

  logout()
  {
    this._loginService.Logout().subscribe(() =>
    {
      this._loginService.setUser({} as IUser);
      this.router.navigateByUrl('/login');
    });
  }
}
