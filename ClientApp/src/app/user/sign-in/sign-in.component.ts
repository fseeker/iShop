import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { IUser, IUserCredentials } from '../user.model';
import { UserService } from '../user.service';
import { Store } from '@ngrx/store';
import { set } from 'src/app/store/app-user.actions';

@Component({
  selector: 'bot-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css'],
})
export class SignInComponent {
  credentials: IUserCredentials = { userName: '', password: '' };
  signInError: boolean = false;

  constructor(private userService: UserService, private router: Router, private store: Store<{ User: IUser }>) { }

  signIn() {
    this.signInError = false;
    this.userService.signIn(this.credentials).subscribe(
      (data) => {
        if (data.token) {
          //set user in store
          this.store.dispatch(set({UserProfile: data}));

          //todo-make session as signed in user
          this.router.navigate(['/catalog']);
        } else {
          this.signInError = true;
        }
      }
    );
  }

}
