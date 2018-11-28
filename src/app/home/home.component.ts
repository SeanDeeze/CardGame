import { Component, OnInit, OnDestroy } from '@angular/core';
import { IPlayer } from '../shared/models/player';
import { LoginService } from '../services/login.service';
import { interval } from 'rxjs/observable/interval';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, OnDestroy {

  activePlayers: IPlayer[] = [];
  interval = 5000;
  source: Subscription;
  constructor(private _loginService: LoginService) { }

  ngOnInit() {
     this.source = interval(this.interval).subscribe(() => {
      if (this._loginService.isPlayerLoggedIn()) {
        this.updateActivePlayers();
      }
    });
  }

  ngOnDestroy() {
    this.source.unsubscribe();
  }

  updateActivePlayers() {
    this._loginService.GetLoggedInPlayers().subscribe(result => {
      if (result.status === true) {
        this.activePlayers = result.returnData[0] as IPlayer[];
      }
    });
  }
}
