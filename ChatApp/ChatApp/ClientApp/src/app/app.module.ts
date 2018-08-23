import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router'
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { LoginService } from './Service/LoginService';
import { HttpClientModule } from "@angular/common/http";
import { RoutingModuleModule } from './routing-module.module';
import { MessageService } from './Service/MessageService';
import { ChatroomComponent } from './chatroom/chatroom.component';
import { ChatListComponent } from './chat-list/chat-list.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule,
    RoutingModuleModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: 'home', component: HomeComponent },
      { path: 'login', component: LoginComponent },
      { path: '', component: HomeComponent },
      { path: 'reg', component: RegisterComponent },
      { path: 'chatroom', component: ChatroomComponent },
      { path: 'chatlist', component: ChatListComponent },
      //{ useHash: true }    

    ])
  ],
  providers: [LoginService, MessageService],
  bootstrap: [AppComponent]
})
export class AppModule { }
