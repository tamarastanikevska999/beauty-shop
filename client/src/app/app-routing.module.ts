import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeOverviewComponent } from './home/components/home-overview/home-overview.component';

const routes: Routes = [
  {path: '', component: HomeOverviewComponent},
  {path: 'products', loadChildren: () => import('./shop/shop.module').then(m => m.ShopModule)},
  {path: '**', redirectTo: '', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
