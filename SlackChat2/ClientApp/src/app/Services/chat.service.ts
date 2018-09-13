import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';
import signalR = require('@aspnet/signalr');
import { HubConnection } from '@aspnet/signalr';

import { ActivatedRoute } from '@angular/router';


@Injectable()
export class ChatService {
  connId: any;
  message: string;
  messages = [];
  private _hubConnection: HubConnection | undefined;
  public async: any;

  constructor(private http: Http) {
    this._hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:44316/chathub')
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this._hubConnection.start().then(() => {

      this._hubConnection.invoke('upData');

    }).catch(err => console.error(err.toString()));

    this._hubConnection.on('send', (data: any) => {
      const received = ` ${data}`;
      this.message = received;
      console.log('received:', this.message);
      this.messages.push(received);
      console.log(this.messages);
      return this.messages;
    });

    this._hubConnection.on('upData', (data: any) => {
      this.connId = data;
      console.log('conn id: ', this.connId);
    });
  }

  sendMessage(data: any, connId: any) {
    if (this._hubConnection) {
      console.log('calling send', connId);
      var temp = this._hubConnection.invoke('Send', data, connId);
      console.log("temp", temp);
      return this.messages;
    }
  }
}
