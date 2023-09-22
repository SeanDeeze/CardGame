import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {LoginService} from './services/login.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit
{
  displayLogin = true;

  constructor(public _loginService: LoginService, private router: Router) { }

  async ngOnInit()
  {
    if (!this._loginService.isPlayerLoggedIn())
    {
      this.router.navigateByUrl('/logout');
    }
    else if (this._loginService.getUser() == null)
    {
      this.router.navigateByUrl('/login');
    }
  }
}
