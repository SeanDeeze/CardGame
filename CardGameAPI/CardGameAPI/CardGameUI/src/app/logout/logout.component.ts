import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {LoginService} from '../services/login.service';
import {IUser} from '../shared/models/player';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent implements OnInit
{

  constructor(public _loginService: LoginService, private router: Router) { }

  ngOnInit(): void
  {
    this._loginService.Logout().subscribe(() =>
    {
      this._loginService.setUser({} as IUser);
      this.router.navigateByUrl('/login');
    });
  }
}
