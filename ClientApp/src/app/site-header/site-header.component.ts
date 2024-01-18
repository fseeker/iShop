import { Component, OnInit } from '@angular/core';
import { IUser } from '../user/user.model';
import { UserService } from '../user/user.service';
import { Store } from '@ngrx/store';

@Component({
  selector: 'bot-site-header',
  templateUrl: './site-header.component.html',
  styleUrls: ['./site-header.component.css'],
})
export class SiteHeaderComponent {
  user: IUser | null = {} as IUser;
  showSignOutMenu: boolean = false;

  constructor(private store:Store<{ User: IUser }>, private userSvc : UserService) {
    this.store.select('User').subscribe((data) => {
      this.user = data;
    })
   }

  toggleSignOutMenu() {
    this.showSignOutMenu = !this.showSignOutMenu;
  }

  signOut() {
    this.userSvc.signOut();
    this.user = null;
    window.localStorage.clear();
    this.showSignOutMenu = false;
  }

  isUserAuthorized() : boolean{
    //todo
    if(!this.user){
      return false;
    }
    return this.user?.role > 0;
  }
}
