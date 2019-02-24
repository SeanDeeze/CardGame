import { Component, AfterViewInit } from '@angular/core';
import { IPlayer } from '../shared/models/player';
import { LoginService } from '../services/login.service';
import { CGMessage } from '../shared/models/CGMessage';
import { isNullOrUndefined } from 'util';
import { Router } from '@angular/router';
import { MenuItem } from 'primeng/components/common/menuitem';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements AfterViewInit {
  displayLogin = true;
  loginPlayer: IPlayer = {} as IPlayer;

  constructor(private _loginService: LoginService, private router: Router) { }

  ngAfterViewInit() {
    if (localStorage.getItem('id')) {
      this._loginService.setPlayer({
        id: !isNullOrUndefined(localStorage.getItem('id')) ? Number(localStorage.getItem('id')) : null,
        userName: !isNullOrUndefined(localStorage.getItem('username')) ? localStorage.getItem('username') : null,
        LastActivity: new Date(), admin: Boolean(localStorage.getItem('admin')), currentGame: null, wins: 0, points: 0
      });
    }
  }

  login() {
    this._loginService.Login(this.loginPlayer).subscribe(result => {
      result = result as CGMessage;
      if (result.status === true) {
        const p: IPlayer = result.returnData[0] as IPlayer;
        Promise.resolve(null).then(() => this._loginService.setPlayer(p)); // Called as promise to avoid ngChangeDetection error
        localStorage.setItem('id', p.id.toString());
        localStorage.setItem('username', p.userName);
        localStorage.setItem('admin', p.admin.toString());

        const menuItems = p.admin ? [
          { label: 'Home', icon: 'fa fa-fw fa-home', routerLink: 'home' },
          { label: 'Games', icon: 'fa fa-fw fa-gamepad', routerLink: 'games' },
          { label: 'Cards', icon: 'fa fa-fw fa-book', routerLink: 'cards' },
          { label: 'Rules', icon: 'fa fa-fw fa-question', routerLink: 'rules' },
          { label: 'Logout', icon: 'fa fa-fw fa-sign-out', command: () => { window.location.reload(); } }
        ] as MenuItem[] : [
          { label: 'Home', icon: 'fa fa-fw fa-home', routerLink: 'home' },
          { label: 'Games', icon: 'fa fa-fw fa-gamepad', routerLink: 'games' },
          { label: 'Rules', icon: 'fa fa-fw fa-question', routerLink: 'rules' },
          { label: 'Logout', icon: 'fa fa-fw fa-sign-out', command: () => { window.location.reload(); } }
        ] as MenuItem[];
        this._loginService.setMenuItems(menuItems);

        if (p.currentGame != null) {
          this.router.navigateByUrl('/game');
        }
        this.router.navigateByUrl('/home');
      }
    });
  }
}
