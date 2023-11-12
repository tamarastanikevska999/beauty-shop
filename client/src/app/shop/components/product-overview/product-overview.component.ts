import { Component, Input } from '@angular/core';
import { ProductService } from '../../services/product.service';

@Component({
  selector: 'app-product-overview',
  templateUrl: './product-overview.component.html',
  styleUrls: ['./product-overview.component.scss']
})
export class ProductOverviewComponent {
  @Input() product: any; // Change to your actual product model
  newComment: string = '';

  constructor(
    protected productService: ProductService
  ) { }

  rateProduct(rating: number): void {
  }

  addComment(): void {
  }
}
