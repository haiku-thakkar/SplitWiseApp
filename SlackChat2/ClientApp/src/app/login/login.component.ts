import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../Services/user.service';
import { ChatService } from '../Services/chat.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  messages: any;
  getUser: any;

  user = {
    id: 0,
    name: '',
    password: '',
    connectionId: '',
    status: true
  };

  id: any;
  name: any;
  password: any;
  connId: any;
  connectionId: any;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private userService: UserService,
    private chatservice: ChatService) {
    this.router.events.subscribe(event => {
    });
  }

  ngOnInit() {

  }


  submit() {
    this.connectionId = this.chatservice.connId;
    console.log("on submit", this.connectionId);
    this.user.connectionId = this.connectionId;
    console.log("user=", this.user);
    if (this.id != 0) {
      this.userService.update(this.user)
        .subscribe(x => {
          console.log(x),
            this.router.navigate(['/home/', x.id])
        });
    }
    else {
      this.userService.create(this.user)
        .subscribe(x => {
          console.log(x),
            this.router.navigate(['/home/', x.id])
        });
    }
  }


}
