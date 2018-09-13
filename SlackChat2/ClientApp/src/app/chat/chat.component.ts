import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
import { UserService } from '../Services/user.service';
import { MessageService } from '../Services/message.service';
import { ChatService } from '../Services/chat.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {

  date: Date;
  message: any;
  messages = [];
  messageList: any;
  public now: Date = new Date();

  msgObj = {
    sender: 0,
    receiver: 0,
    content: '',
    dateTime: ''
  }

  sender = {
    sid: 0,
    name: '',
    connectionId: '',

  }

  recvr = {
    rid: 0
  };
  sid: any;
  rid: any;
  sname: any;
  connId: any;
  rconnId: any;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private userService: UserService,
    private messageService: MessageService,
    private chatService: ChatService) {
    setInterval(() => {
      this.date = new Date();
    }, 1);

    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        // this._activatedRoute.snapshot is up to date
      }
    });


    this.rid = route.snapshot.params['rid'];
    this.sid = route.snapshot.params['sid'];

    route.params.subscribe(p => {
      this.recvr.rid = +p['rid'];
    }, err => {
      if (err.status == 404)
        this.router.navigate(['/home']);
    });
    route.params.subscribe(p => {
      this.sender.sid = +p['sid'];
    }, err => {
      if (err.status == 404)
        this.router.navigate(['/home']);
    });
  }

  ngOnInit() {
    this.chatService.messages = [];
    this.userService.getuser(this.rid)
      .subscribe(b => {
        this.recvr = b;
        this.connId = b.connectionId;
        console.log('ng recv conn id', this.connId);
      });

    console.log(this.messageList);
    console.log(this.sender.sid, this.recvr.rid);

    this.userService.getuser(this.sid)
      .subscribe(b => {
        this.sender = b;
      });

    this.messageService.getMessages()
      .subscribe(b => {
        this.messageList = b;

      });

  }

  public sendMessage(): void {

    this.msgObj.dateTime = this.getNowDate();

    console.log("time: ", this.msgObj.dateTime);

    this.msgObj.sender = this.sid;
    this.msgObj.receiver = this.rid;
    this.msgObj.content = this.message;

    //    console.log('msgobe: ' , this.msgObj);

    const data = this.sender.name + ' : ' + ` ${this.message}`;
    console.log('sending msg to: ', this.connId);
    this.messages = this.chatService.sendMessage(data, this.connId);

    this.messageService.create(this.msgObj)
      .subscribe(x => {
        console.log(x);

      });
    this.message = '';
  }

  getNowDate() {
    var returnDate = "";
    var today = new Date();

    var dd = today.getDate();
    var mm = today.getMonth() + 1;
    var yyyy = today.getFullYear();
    var hour = today.getHours();
    var min = today.getMinutes();

    if (hour < 10) {
      returnDate += `0${hour}-`;
    } else {
      returnDate += `${hour}-`;
    }

    if (min < 10) {
      returnDate += `0${min} `;
    } else {
      returnDate += `${min} `;
    }

    if (dd < 10) {
      returnDate += `0${dd}-`;
    } else {
      returnDate += `${dd}-`;
    }

    if (mm < 10) {
      returnDate += `0${mm}-`;
    } else {
      returnDate += `${mm}-`;
    }

    returnDate += yyyy;
    return returnDate;
  }

}
