import { Component, OnInit, OnDestroy } from '@angular/core';
import { IPlayer } from '../shared/models/player';
import { LoginService } from '../services/login.service';
import { interval } from 'rxjs/observable/interval';
import { Subscription } from 'rxjs';
import { isNullOrUndefined } from 'util';
import { SignalRService } from '../services/signal-r.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, OnDestroy {

  activePlayers: IPlayer[] = [];
  interval = 1500;
  source: Subscription;
  constructor(private _loginService: LoginService, public _signalRService: SignalRService) { }

  ngOnInit() {
    this.source = interval(this.interval).subscribe(() => {
      if (this._loginService.isPlayerLoggedIn()) {
        this._signalRService.connect('');
      }
    });
  }

  ngOnDestroy() {
    if (isNullOrUndefined(this.source)) {
      this._signalRService.disconnect();
    }
  }
}
