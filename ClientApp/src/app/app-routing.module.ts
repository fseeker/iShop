import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CartComponent } from './cart/cart.component';
import { CatalogComponent } from './catalog/catalog.component';
import { HomeComponent } from './home/home.component';
import { SignInComponent } from './user/sign-in/sign-in.component';
import { TemplateFormControlsComponent } from './user/template-form-controls/template-form-controls.component';
import { AddProductComponent } from './add-product/add-product.component';
import { ViewStockComponent } from './view-stock/view-stock.component';
import { SignUpComponent } from './user/sign-up/sign-up.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent, title: "Home" },
  { path: 'catalog', component: CatalogComponent, title: "Catalog" },
  { path: 'cart', component: CartComponent, title: "Cart" },
  { path: 'add-product', component: AddProductComponent, title: "Add product"  },
  { path: 'view-stock', component: ViewStockComponent, title: "Stock Management"  },
  { path: 'sign-in', component: SignInComponent },
  { path: 'sign-up', component: SignUpComponent},
  { path: 'form-controls', component: TemplateFormControlsComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
