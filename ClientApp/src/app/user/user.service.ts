import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, map, Observable } from 'rxjs';

import { IUser, IUserCredentials } from './user.model';
import { Store } from '@ngrx/store';
import { reset } from '../store/app-user.actions';

@Injectable({
  providedIn: 'root',
})
export class UserService {

  constructor(private http: HttpClient, private store: Store<{ User: IUser }>) {

  }

  signIn(credentials: IUserCredentials): Observable<IUser> {
    return this.http
      .post<IUser>('/api/user/login', credentials);
  }

  signUp(credentials: IUserCredentials): Observable<IUser> {
    return this.http
      .post<IUser>('/api/user/signup', credentials);
  }

  signOut() {
    this.store.dispatch(reset());
  }

  getToken(): Observable<IUser> {
    return this.store.select('User');
  }
}
