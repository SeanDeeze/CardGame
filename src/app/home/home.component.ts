import { Component, OnInit } from '@angular/core';
import { IPlayer } from '../shared/models/player';
import { LoginService } from '../services/login.service';
import { interval } from 'rxjs/observable/interval';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  activePlayers: IPlayer[] = [];
  interval = 1000; // every 1 sec
  constructor(private _loginService: LoginService) { }

  ngOnInit() {
    interval(this.interval).subscribe(result => {
      this.updateActivePlayers();
    });
  }

  updateActivePlayers() {
    this._loginService.GetLoggedInPlayers().subscribe(result => {
      if (result.status === true) {
        this.activePlayers = result.returnData[0] as IPlayer[];
      }
    });
  }
}
