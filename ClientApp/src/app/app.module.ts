import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { CatalogComponent } from './catalog/catalog.component';
import { SiteHeaderComponent } from './site-header/site-header.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { CartComponent } from './cart/cart.component';
import { AppRoutingModule } from './app-routing.module';
import { SignInComponent } from './user/sign-in/sign-in.component';
import { TemplateFormControlsComponent } from './user/template-form-controls/template-form-controls.component';
import { AddProductComponent } from './add-product/add-product.component';
import { ViewStockComponent } from './view-stock/view-stock.component';
import { SignUpComponent } from './user/sign-up/sign-up.component';
import { StockDetailComponent } from './view-stock/stock-detail/stock-detail.component';
import { StoreModule } from '@ngrx/store';
import { appUserReducer } from './store/app-user.reducer';

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        CatalogComponent,
        SiteHeaderComponent,
        ProductDetailsComponent,
        CartComponent,
        SignInComponent,
        TemplateFormControlsComponent,
        AddProductComponent,
        ViewStockComponent,
        SignUpComponent,
        StockDetailComponent,
    ],
    providers: [],
    bootstrap: [AppComponent],
    imports: [BrowserModule, HttpClientModule, AppRoutingModule, FormsModule, StoreModule.forRoot({ User: appUserReducer})]
})
export class AppModule { }
