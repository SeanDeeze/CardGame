import { Component, OnInit } from '@angular/core';
import { LoginService } from './services/login.service';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'app';
  menuItems: MenuItem[];

  constructor(public _loginService: LoginService) { }

  ngOnInit() {
    this.menuItems = [
      {label: 'Home', icon: 'fa fa-fw fa-home'},
      {label: 'Games', icon: 'fa fa-fw fa-gamepad'},
      {label: 'Cards', icon: 'fa fa-fw fa-book'},
      {label: 'About', icon: 'fa fa-fw fa-question'},
      {label: 'Logout', icon: 'fa fa-fw fa-sign-out'}
  ];
  }
}
