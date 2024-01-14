import { Component } from '@angular/core';
import { IProduct } from '../catalog/product.model';
import { ProductService } from '../catalog/product.service';

@Component({
  selector: 'is-view-stock',
  templateUrl: './view-stock.component.html',
  styleUrls: ['./view-stock.component.css']
})
export class ViewStockComponent {
  products: IProduct[] = [];

  constructor(productSvc: ProductService) {
    productSvc.getProducts().subscribe((products) => this.products = products);
  }

}
