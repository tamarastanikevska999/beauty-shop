import { SharedModule } from './../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductsListComponent } from './components/products-list/products-list.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatSliderModule } from '@angular/material/slider';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatListModule } from '@angular/material/list';
import { ReactiveFormsModule } from '@angular/forms';
import { ProductOverviewComponent } from './components/product-overview/product-overview.component';
import { FormsModule } from '@angular/forms';
import {MatPaginatorModule} from '@angular/material/paginator';
import { ShopRoutingModule } from './shop-routing.module';
import { MatDialogModule } from '@angular/material/dialog';
import { MatTableModule } from '@angular/material/table';
import { RouterModule } from '@angular/router';
import { ProductItemComponent } from './components/product-item/product-item.component';


@NgModule({
  declarations: [
    ProductsListComponent,
    ProductOverviewComponent,
    ProductItemComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    MatToolbarModule,
    MatButtonModule,
    MatCardModule,
    MatFormFieldModule,
    MatSelectModule,
    MatSliderModule,
    MatButtonToggleModule,
    MatListModule,
    FormsModule,
    MatPaginatorModule,
    MatDialogModule,
    MatTableModule,
    SharedModule
  ],
  exports: [
    ShopRoutingModule
  ]
})
export class ShopModule { }
