import {HttpClientModule} from '@angular/common/http';
import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {BrowserModule} from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {RouterModule, Routes} from '@angular/router';
import {ButtonModule} from 'primeng/button';
import {CardModule} from 'primeng/card';
import {CheckboxModule} from 'primeng/checkbox';
import {DataViewModule} from 'primeng/dataview';
import {DialogModule} from 'primeng/dialog';
import {DropdownModule} from 'primeng/dropdown';
import {FileUploadModule} from 'primeng/fileupload';
import {InputTextModule} from 'primeng/inputtext';
import {PickListModule} from 'primeng/picklist';
import {TableModule} from 'primeng/table';
import {TabMenuModule} from 'primeng/tabmenu';
import {TabViewModule} from 'primeng/tabview';
import {AppComponent} from './app.component';
import {CardsComponent} from './cards/cards.component';
import {GameComponent} from './game/game.component';
import {GamesComponent} from './games/games.component';
import {HomeComponent} from './home/home.component';
import {LoginComponent} from './login/login.component';
import {LogoutComponent} from './logout/logout.component';
import {LoggingService} from './services/logging.service';
import {LoginService} from './services/login.service';

const appRoutes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'home', component: HomeComponent},
  {path: 'login', component: LoginComponent},
  {path: 'game/:id', component: GameComponent},
  {path: 'games', component: GamesComponent},
  {path: 'cards', component: CardsComponent},
  {path: 'logout', component: LogoutComponent}
];

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    CardsComponent,
    GamesComponent,
    HomeComponent,
    GameComponent,
    LogoutComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    ButtonModule,
    CardModule,
    CheckboxModule,
    DataViewModule,
    DialogModule,
    DropdownModule,
    FileUploadModule,
    FormsModule,
    HttpClientModule,
    InputTextModule,
    PickListModule,
    TableModule,
    TabMenuModule,
    TabViewModule,
    ReactiveFormsModule,
    RouterModule.forRoot(appRoutes, {useHash: false})
  ],
  providers: [
    LoginService,
    LoggingService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }


