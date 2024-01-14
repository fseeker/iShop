import { Component, Input } from '@angular/core';
import { IProduct } from 'src/app/catalog/product.model';

@Component({
  selector: 'is-stock-details',
  templateUrl: './stock-detail.component.html',
  styleUrls: ['./stock-detail.component.css']
})
export class StockDetailComponent {
  @Input() product!: IProduct;
  value: number = 0;

  adjustStock(productId: number) {

  }
}
