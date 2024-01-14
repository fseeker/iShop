import { Component } from '@angular/core';
import { IProduct } from '../catalog/product.model';
import { ProductService } from '../catalog/product.service';

@Component({
  selector: 'bot-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent {
  addProduct: IProduct = {
    id: 0,
    name : '',
    category: '',
    imageName: '',
    description: '',
    price: 0.00,
    discount: 0.00,
    stock: 1
  }

  constructor(private productSvc: ProductService){

  }

  AddProduct(){
    this.productSvc.addProduct(this.addProduct).subscribe((data) => {
      if (data !== 0){
        console.log('success');
      }else{
        console.log('fail');
      }
    });
  }
}
