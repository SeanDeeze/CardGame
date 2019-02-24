import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { isNullOrUndefined } from 'util';
import { SignalRService } from '../services/signal-r.service';
import { LoginService } from '../services/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, OnDestroy {
  interval = 1500;
  source: Subscription;
  constructor(public _signalRService: SignalRService, private _loginService: LoginService, private router: Router) { }

  ngOnInit() {
    if (!this._loginService.isPlayerLoggedIn()) {
      this.router.navigateByUrl('/login');
    }
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
