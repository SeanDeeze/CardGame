import { Component } from '@angular/core';
import { IPlayer } from '../shared/models/player';
import { LoginService } from '../services/login.service';
import { CGMessage } from '../shared/models/CGMessage';
import { Router } from '@angular/router';
import { MenuItem } from 'primeng/components/common/menuitem';
import { SignalRService } from '../services/signal-r.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  displayLogin = true;
  loginPlayer: IPlayer = {} as IPlayer;
  showLogin = true;

  constructor(private _loginService: LoginService, private router: Router, private _signalRService: SignalRService) { }


  login() {
    this._loginService.Login(this.loginPlayer).subscribe(result => {
      result = result as CGMessage;
      if (result.status === true) {
        const p: IPlayer = result.returnData[0] as IPlayer;
        Promise.resolve(null).then(() => this._loginService.setPlayer(p)); // Called as promise to avoid ngChangeDetection error 

        const menuItems = p.admin ? [
          { label: 'Home', icon: 'fa fa-fw fa-home', routerLink: 'home' },
          { label: 'Games', icon: 'fa fa-fw fa-gamepad', routerLink: 'games' },
          { label: 'Cards', icon: 'fa fa-fw fa-book', routerLink: 'cards' },
          { label: 'Rules', icon: 'fa fa-fw fa-question', routerLink: 'rules' },
          {
            label: 'Logout', icon: 'fa fa-fw fa-sign-out', command: () => {
              this._loginService.Logout().subscribe(() => {
                this._loginService.setPlayer({} as IPlayer);
                this.router.navigateByUrl('/login');
              });
            }
          }
        ] as MenuItem[] : [
          { label: 'Home', icon: 'fa fa-fw fa-home', routerLink: 'home' },
          { label: 'Games', icon: 'fa fa-fw fa-gamepad', routerLink: 'games' },
          { label: 'Rules', icon: 'fa fa-fw fa-question', routerLink: 'rules' },
          {
            label: 'Logout', icon: 'fa fa-fw fa-sign-out', command: () => {
              this._loginService.Logout().subscribe(() => {
                const currentGame = this._loginService.getPlayer().currentGame;
                if (currentGame !== null && currentGame !== undefined) {
                  this._signalRService.removeFromGroup(currentGame.id);
                }
                this._loginService.setPlayer({} as IPlayer);
                this.router.navigateByUrl('/login');
              });
            }
          }
        ] as MenuItem[];
        this._loginService.setMenuItems(menuItems);

        this._signalRService.connect('');

        if (p.currentGame != null) {
          this.router.navigateByUrl('/game');
        }
        this.router.navigateByUrl('/home');
      }
    });
  }
}
