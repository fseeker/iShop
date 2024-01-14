import { Component } from '@angular/core';
import { IUserCredentials } from '../user.model';
import { UserService } from '../user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'bot-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent {

  signInError: boolean = false;

  credentials: any = {
    userName: '',
    password: '',
    confirmPassword: ''
  }
  constructor(private userService: UserService, private router: Router){

  }

  signUp(){
    this.signInError = false;

    this.userService.signUp(this.credentials).subscribe(
      (data) => {
        if(data){
          //todo-make session as signed in user
          this.router.navigate(['/catalog'])
        }else{
          this.signInError = true;
        }
      }
    );
  }
}
