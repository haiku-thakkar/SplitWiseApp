import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ChatComponent } from './chat/chat.component';
import { LoginComponent } from './login/login.component';
import { ChatService } from './Services/chat.service';
import { UserService } from './Services/user.service';
import { MessageService } from './Services/message.service';
import { HttpModule } from '@angular/http';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ChatComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    HttpModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: LoginComponent, pathMatch: 'full' },
      { path: 'home/:sid', component: HomeComponent },
      { path: 'login', component: LoginComponent },
      { path: 'chat', component: ChatComponent },
      { path: 'chat/:sid/:rid', component: ChatComponent },
      { path: '**', component: LoginComponent },
    ])
  ],
  providers: [ChatService, UserService, MessageService],
  bootstrap: [AppComponent]
})
export class AppModule { }
