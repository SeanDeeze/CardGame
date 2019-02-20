import { Component, OnInit } from '@angular/core';
import { LoginService } from './services/login.service';
import { MenuItem } from 'primeng/api';
import { IPlayer } from './shared/models/player';
import { Router } from '@angular/router';
import { interval } from 'rxjs/observable/interval';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  displayLogin = true;
  menuItems: MenuItem[];

  constructor(public _loginService: LoginService, private router: Router) { }

  ngOnInit() {
    this.menuItems = this._loginService.getMenuItems();

    interval(5000).subscribe(() => {
      if (this._loginService.isPlayerLoggedIn()) {
        this._loginService.KeepAlive().subscribe(() => { });
      }
    });
  }

  logout() {
    this._loginService.Logout().subscribe(result => {
      if (result.status === true) {
        localStorage.removeItem('id');
        localStorage.removeItem('username');
        this._loginService.setPlayer({} as IPlayer);
        this.router.navigateByUrl('/home');
      }
    });
  }
}
