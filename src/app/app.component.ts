import { Component, OnInit } from '@angular/core';
import { LoginService } from './services/login.service';
import { IPlayer } from './shared/models/player';
import { Router } from '@angular/router';
import { interval } from 'rxjs/observable/interval';
import { SignalRService } from './services/signal-r.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  displayLogin = true;

  constructor(public _loginService: LoginService, private router: Router, private _signalRService: SignalRService) { }

  ngOnInit() {
    this.logout();
    interval(5000).subscribe(() => {
      this._loginService.KeepAlive().subscribe(() => { });
    });
  }

  logout() {
    this._loginService.Logout().subscribe(result => {
      if (result.status === true) {
        localStorage.removeItem('id');
        localStorage.removeItem('username');
        this._loginService.setPlayer({} as IPlayer);
        this._signalRService.disconnect();
        this.router.navigateByUrl('/home');
      }
    });
  }
}
