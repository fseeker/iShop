import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';

import { IProduct } from '../catalog/product.model';
import { ICartItem } from './cartItem.model';
import { UserService } from '../user/user.service';
import { IUser } from '../user/user.model';
import { Route, Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private cart: BehaviorSubject<IProduct[]> = new BehaviorSubject<IProduct[]>([]); //i dont understand behavioursubject
  public user: IUser | null = null;

  constructor(private http: HttpClient, private userService: UserService, private route:Router) {
    this.userService.getToken().subscribe((data) => this.user = data);
  }

  

  getCart(): Observable<ICartItem[]> {
    // if(this.user?.token == null){
    //   this.route.navigate(['/sign-in']);
    // } 
    const headers = new HttpHeaders({
      //'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.user?.token}`
    });
    return this.http.get<ICartItem[]>('/api/product/getCart?', {headers : headers});
  }

  addToCart(product: IProduct) {
    if(this.user?.token == '') this.route.navigate(['/sign-in']);
    const headers = new HttpHeaders({
      //'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.user?.token}`
    });

    const requestBody = {
      id: product.id,
      quantity: 1
    }

    this.http.post<string>('/api/product/addToCart', requestBody, {headers : headers}).subscribe((data) => {
      console.log(data);
    });
  }

  removeFromCart(product: IProduct) {
    //TODO
  }
}
