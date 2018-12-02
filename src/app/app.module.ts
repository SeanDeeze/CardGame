import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';

import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { CheckboxModule } from 'primeng/checkbox';
import { DialogModule } from 'primeng/dialog';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { PickListModule } from 'primeng/picklist';
import { TabMenuModule } from 'primeng/tabmenu';
import { TableModule } from 'primeng/table';

import { LoginService } from './services/login.service';
import { LoggingService } from './services/logging.service';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { CardsComponent } from './cards/cards.component';
import { GamesComponent } from './games/games.component';
import { HomeComponent } from './home/home.component';
import { RulesComponent } from './rules/rules.component';

const appRoutes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'games', component: GamesComponent },
  { path: 'cards', component: CardsComponent },
  { path: 'rules', component: RulesComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    CardsComponent,
    GamesComponent,
    HomeComponent,
    RulesComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    ButtonModule,
    CardModule,
    CheckboxModule,
    DialogModule,
    DropdownModule,
    FormsModule,
    HttpClientModule,
    InputTextModule,
    PickListModule,
    TableModule,
    TabMenuModule,
    RouterModule.forRoot(appRoutes, { useHash: true })
  ],
  providers: [
    LoginService,
    LoggingService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }


