import { Component, OnInit, OnDestroy } from '@angular/core';
import { LoginService } from './services/login.service';
import { SignalRService } from './services/signal-r.service';
import { Router } from '@angular/router';
import { IPlayer } from './shared/models/player';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {
  displayLogin = true;

  constructor(public _loginService: LoginService, private _signalRService: SignalRService, private router: Router) { }

  async ngOnInit() {
    if (!this._loginService.isPlayerLoggedIn()) {
      this.logout();
    } else if (this._loginService.getPlayer() == null) {
      this.router.navigateByUrl('/login');
    }
    await this._signalRService.connect('');
  }

  ngOnDestroy() {
    this.logout();
  }

  logout() {
    this._loginService.Logout().subscribe(result => {
      const currentGame = this._loginService.getPlayer().currentGame;
      if (currentGame !== undefined) {
        this._signalRService.removeFromGroup(currentGame.id);
      }
      this._loginService.setPlayer({} as IPlayer);
      this.router.navigateByUrl('/login');
    });
  }
}
