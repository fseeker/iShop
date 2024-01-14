import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { IUserCredentials } from '../user.model';
import { UserService } from '../user.service';

@Component({
  selector: 'bot-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css'],
})
export class SignInComponent {
  credentials: IUserCredentials = { userName: '', password: '' };
  signInError: boolean = false;

  constructor(private userService: UserService, private router: Router) { }

  signIn() {
    this.signInError = false;
    this.userService.signIn(this.credentials).subscribe(
      (data) => {
        if(data.token){
          //todo-make session as signed in user
          
          this.router.navigate(['/catalog'])
        }else{
          this.signInError = true;
        }
      }
    );
  }

}
