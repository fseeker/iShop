import { Component, OnInit } from '@angular/core';
import { IProduct } from '../catalog/product.model';
import { CartService } from './cart.service';
import { ICartItem } from './cartItem.model';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'bot-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css'],
})
export class CartComponent implements OnInit {
  private cart: ICartItem[] = [];
  constructor(private cartService: CartService, private route: Router) { }

  ngOnInit() {
    this.cartService.getCart().subscribe({
      next: (cart) => (this.cart = cart),
      error: (err) => {
        let error = err as HttpErrorResponse
        if(error.status == 401){
          this.route.navigate(['/sign-in']);
        }else{
          //TODO
          console.log(err);
        }
      }
    });
  }

  get cartItems() {
    return this.cart;
  }

  get cartTotal() {
    return this.cart.reduce((prev, next) => {
      let discount = next.discount && next.discount > 0 ? 1 - next.discount : 1;
      return prev + next.price * discount;
    }, 0);
  }

  removeFromCart(product: ICartItem) {
    //this.cartService.remove(product);
  }

  getImageUrl(product: ICartItem) {
    if (!product) return '';
    return '/assets/images/robot-parts/' + product.imageName;
  }
}
