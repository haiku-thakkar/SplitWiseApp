import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../Services/user.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit{
  
  user = {
    id: 0,
    name: '',
    status: '',
  }
  id: any;
  name: any;
  users: any;
  constructor(private route: ActivatedRoute, private router: Router,
    private userService: UserService) {
    this.id = route.snapshot.params['sid'];
  }


  ngOnInit(): void {
    this.userService.getUsers()
      .subscribe(users => this.users = users);

    this.userService.getuser(this.id)
      .subscribe(user => this.user = user);

    console.log(this.users);

  }
  logOut() {

    this.user.status = 'False';
    this.userService.update(this.user)
      .subscribe(x => {
        console.log(x),
          this.router.navigate(['']);
      });
  }
}

