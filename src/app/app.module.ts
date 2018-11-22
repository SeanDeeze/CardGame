import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';

import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { TabMenuModule } from 'primeng/tabmenu';

import { LoginService } from './services/login.service';
import { LoggingService } from './services/logging.service';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { CardsComponent } from './cards/cards.component';
import { GamesComponent } from './games/games.component';

const appRoutes: Routes = [
  { path: '', component: AppComponent },
  { path: 'home', component: AppComponent },
  { path: 'login', component: LoginComponent },
  { path: 'games', component: GamesComponent },
  { path: 'cards', component: CardsComponent },
  { path: 'about', component: CardsComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    CardsComponent,
    GamesComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    ButtonModule,
    DialogModule,
    FormsModule,
    HttpClientModule,
    InputTextModule,
    TabMenuModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [
    LoginService,
    LoggingService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }


