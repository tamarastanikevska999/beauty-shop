import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ErrorDialogComponent } from './components/error-dialog/error-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { OrderTotalsComponent } from './components/order-totals/order-totals.component';

@NgModule({
  declarations: [
    ErrorDialogComponent,
    OrderTotalsComponent
  ],
  imports: [
    CommonModule,
    MatDialogModule
  ],
  exports: [
    ErrorDialogComponent,
    OrderTotalsComponent
  ]
  
})
export class SharedModule { }
