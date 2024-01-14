import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, map, Observable } from 'rxjs';

import { IUser, IUserCredentials } from './user.model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  
  private user: BehaviorSubject<IUser | null>;
  private token:string = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFzZCIsIm5iZiI6MTcwNDgxOTQyMiwiZXhwIjoxNzA1NDI0MjIyLCJpYXQiOjE3MDQ4MTk0MjIsImlzcyI6Imh0dHBzOi8vbXlzaXRlLmNvbSIsImF1ZCI6Imh0dHBzOi8vbXlhdWRpZW5jZS5jb20ifQ.tOIxpKw6FX1n74lr-7cyEFYmx8mMPe-yGTMIrzA_l-k';
  constructor(private http: HttpClient) {
    this.user = new BehaviorSubject<IUser | null>(null);
  }

  getUser(): Observable<IUser | null> {
    return this.user;
  }

  signIn(credentials: IUserCredentials): Observable<IUser> {
    return this.http
      .post<IUser>('/api/user/login', credentials);
  }

  signUp(credentials: IUserCredentials): Observable<boolean> {
    return this.http
      .post<boolean>('/api/user/signup', credentials);
  }

  signOut() {
    this.user.next(null);
  }

  getToken(): string {
    return this.token;
  }
}
