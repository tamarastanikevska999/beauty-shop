import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { ProductsListComponent } from './components/products-list/products-list.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { AppComponent } from '../app.component';


@NgModule({
  declarations: [
    NavBarComponent,
    ProductsListComponent
  ],
  imports: [
    CommonModule,
    MatToolbarModule,
    MatButtonModule,
    MatCardModule
  ],
  exports: [
    NavBarComponent,
    ProductsListComponent
  ],
  bootstrap: [AppComponent]
})
export class ShopModule { }
