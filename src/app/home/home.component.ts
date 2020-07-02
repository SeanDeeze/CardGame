import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription, timer } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { isNullOrUndefined } from 'util';
import { SignalRService } from '../services/signal-r.service';
import { LoginService } from '../services/login.service';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service'
import { IPlayer } from '../shared/models/player';
import { CGMessage } from '../shared/models/CGMessage';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, OnDestroy {
  interval = 1500;
  source: Subscription;
  users: IPlayer[] = [];
  constructor(public _signalRService: SignalRService, private _loginService: LoginService, private router: Router,
    private _userService: UserService) { }

  ngOnInit() {
    if (!this._loginService.isPlayerLoggedIn()) {
      this.router.navigateByUrl('/login');
    }
    this.source = timer(0, 2000).pipe(
      switchMap(() => this._userService.GetUsers())
    ).subscribe((result: CGMessage) => {
      if (result.status === true) {
        this.users = result.returnData[0] as IPlayer[];
      }
    });
  }

  ngOnDestroy() {
    if (!isNullOrUndefined(this.source)) {
      if (this.source.closed === false) {
        this.source.unsubscribe();
      }
      this.source = null;
    }
  }
}
