import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

@Injectable()
export class UserService {

  message: string;
  messages: any[];
  public async: any;
  connId: any;
  id: any;
  sender: any;
  constructor(private http: Http) { }

  create(user: any) {
    console.log("In create method");
    console.log(user.connectionId);
    return this.http.post('/api/users', user)
      .map(res => res.json());

  }
  getuser(id: any) {
    console.log("in getuser");
    return this.http.get('/api/users/' + id)
      .map(res => res.json());
  }
  update(user: any) {
    console.log(user);
    return this.http.put('/api/users/' + user.id, user)
      .map(res => res.json());
  }
  delete(id: any) {
    return this.http.delete('/api/users/' + id)
      .map(res => res.json());
  }
  getUsers() {
    console.log("in getusers");
    return this.http.get('/api/users')
      .map(res => res.json());
  }

  createMsg(user: any, connId: any, data: any) {
    console.log("In create message method" + user, data);

    return this.http.post('/api/message', user)
      .map(res => res.json());
  }
  getmsg() {
    return this.message;
  }


}

