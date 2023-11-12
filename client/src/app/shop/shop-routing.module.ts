import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductsListComponent } from './components/products-list/products-list.component';
import { ProductOverviewComponent } from './components/product-overview/product-overview.component';

const routes: Routes = [
  {path: '', component: ProductsListComponent},
  {path: ':id', component: ProductOverviewComponent},
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class ShopRoutingModule { }
