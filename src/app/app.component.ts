import { Component, OnInit, OnDestroy } from '@angular/core';
import { LoginService } from './services/login.service';
import { IPlayer } from './shared/models/player';
import { Router } from '@angular/router';
import { SignalRService } from './services/signal-r.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {
  displayLogin = true;

  constructor(public _loginService: LoginService, private router: Router, private _signalRService: SignalRService) { }

  async ngOnInit() {
    if (!this._loginService.isPlayerLoggedIn()) {
      this.logout();
    }
    await this._signalRService.connect('');
  }

  ngOnDestroy() {
    this.logout();
  }

  logout() {
    this._loginService.Logout().subscribe(result => {
      if (result.status === true) {
        localStorage.removeItem('id');
        localStorage.removeItem('username');
        this._loginService.setPlayer({} as IPlayer);
        this._signalRService.disconnect();
        this.router.navigateByUrl('/login');
      }
    });
  }
}
