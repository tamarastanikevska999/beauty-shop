import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-rate-product-dialog',
  templateUrl: './rate-product-dialog.component.html',
  styleUrls: ['./rate-product-dialog.component.scss']
})
export class RateProductDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<RateProductDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { rating: number }
  ) {}

  onNoClick(): void {
    this.dialogRef.close();
  }

  onStarClick(star: number): void {
    this.data.rating = star;
  }
}
