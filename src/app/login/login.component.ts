import { Component, OnInit } from '@angular/core';
import { Player } from '../shared/models/player';
import { LoginService } from '../services/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  display = true;
  player: Player;

  constructor(private _loginService: LoginService) { }

  ngOnInit() {
    this.player = { Id: null, UserName: null, LastActivity: null };
    if (localStorage.getItem('player')) {
      this.player = { Id: Number(localStorage.getItem('id')), UserName: localStorage.getItem('player'), LastActivity: new Date() };
    }
  }

  login() {
    this._loginService.Login(this.player).subscribe(result => {
      console.log(result);
    });
  }

}
