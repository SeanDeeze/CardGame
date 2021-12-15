import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription, timer } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { LoginService } from '../services/login.service';
import { UserService } from '../services/user.service';
import { CGMessage } from '../shared/models/CGMessage';
import { IPlayer } from '../shared/models/player';
import { isNullOrUndefined } from '../shared/utils';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, OnDestroy
{
  interval = 1500;
  source: Subscription;
  users: IPlayer[] = [];
  constructor(private _loginService: LoginService, private router: Router, private _userService: UserService) { }

  ngOnInit()
  {
    if (!this._loginService.isPlayerLoggedIn())
    {
      this.router.navigateByUrl('/login');
    }
    this.source = timer(0, 5000).pipe(
      switchMap(() => this._userService.GetUsers())
    ).subscribe((result: CGMessage) =>
    {
      if (result.status === true)
      {
        this.users = result.returnData[0] as IPlayer[];
      }
    });
  }

  ngOnDestroy()
  {
    if (!isNullOrUndefined(this.source))
    {
      if (this.source.closed === false)
      {
        this.source.unsubscribe();
      }
      this.source = null;
    }
  }
}