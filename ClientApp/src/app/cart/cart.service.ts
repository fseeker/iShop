import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';

import { IProduct } from '../catalog/product.model';
import { ICartItem } from './cartItem.model';
import { UserService } from '../user/user.service';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private cart: BehaviorSubject<IProduct[]> = new BehaviorSubject<IProduct[]>([]); //i dont understand behavioursubject
  userId: number = 10;

  constructor(private http: HttpClient, private userService: UserService) {
    this.http.get<IProduct[]>('/api/product/cart').subscribe({
      next: (cart) => this.cart.next(cart),
    });
  }

  

  getCart(): Observable<ICartItem[]> {
    const token = this.userService.getToken();
    const headers = new HttpHeaders({
      //'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    return this.http.get<ICartItem[]>('/api/product/getCart?UserId='+this.userId, {headers : headers});
  }

  add(product: IProduct) {
    //const newCart = [...this.cart.getValue(), product];
    //this.cart.next(newCart);

    const requestBody = {
      productId: product.id,
      userId: this.userId,
      quantity: 1
    }

    this.http.post('/api/product/addToCart', requestBody).subscribe(() => {
      console.log('added ' + product.name + ' to cart!');
    });
  }

  remove(product: IProduct) {
    let newCart = this.cart.getValue().filter((i) => i !== product);
    this.cart.next(newCart);
    this.http.post('/api/cart', newCart).subscribe(() => {
      console.log('removed ' + product.name + ' from cart!');
    });
  }
}
