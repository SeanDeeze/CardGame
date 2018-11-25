import { Component, OnInit } from '@angular/core';
import { LoginService } from './services/login.service';
import { MenuItem } from 'primeng/api';
import { Player } from './shared/models/player';
import { Router } from '@angular/router';

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
    this.menuItems = [
      { label: 'Home', icon: 'fa fa-fw fa-home', routerLink: 'home' },
      { label: 'Games', icon: 'fa fa-fw fa-gamepad', routerLink: 'games' },
      { label: 'Cards', icon: 'fa fa-fw fa-book',  routerLink: 'cards' },
      { label: 'About', icon: 'fa fa-fw fa-question',  routerLink: 'about' },
      { label: 'Logout', icon: 'fa fa-fw fa-sign-out', command: () => { this.logout(); } }
    ];
  }

  logout() {
    this._loginService.Logout().subscribe(result => {
      if (result.status === true) {
        localStorage.removeItem('id');
        localStorage.removeItem('username');
        this._loginService.setPlayer({} as Player);
        this.router.navigateByUrl('/home');
      }
    });
  }
}
