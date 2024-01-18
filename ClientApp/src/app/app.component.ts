import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { IUser } from './user/user.model';
import { reset, set } from './store/app-user.actions';
import { windowCount } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  constructor(private store:Store<{ User: IUser }>){
    let user = {} as IUser;
    user.userName = window.localStorage.getItem("userName") ?? '';
    user.fullName = window.localStorage.getItem("fullName") ?? ''
    user.token = window.localStorage.getItem("userToken") ?? '';
    if(user.token !== '') this.store.dispatch(set({UserProfile: user}));
    else this.store.dispatch(reset());
  }
}
