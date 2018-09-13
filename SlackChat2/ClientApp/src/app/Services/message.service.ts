import { Injectable } from "@angular/core";
import { Http } from "@angular/http";

@Injectable()
export class MessageService {

  constructor(private http: Http) { }
 
  create(message: any) {
    console.log("In create message method", message);
    return this.http.post('/api/message', message)
      .map(res => res.json());
  }
  getmessage(id: any) {
    console.log("in getmessage");
    return this.http.get('/api/message/' + id)
      .map(res => res.json());
  }

  delete(id: any) {
    return this.http.delete('/api/message/' + id)
      .map(res => res.json());
  }
  getMessages() {
    console.log("in getmessages");
    return this.http.get('/api/message')
      .map(res => res.json());
  }

}
