import { Component, Input, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { Product } from 'src/app/shared/model/product';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-overview',
  templateUrl: './product-overview.component.html',
  styleUrls: ['./product-overview.component.scss']
})
export class ProductOverviewComponent implements OnInit{
  product: Product;
  newComment: string = '';

  constructor(
    private activatedRoute: ActivatedRoute,
    protected productService: ProductService
  ) { }

  ngOnInit(): void {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if(id){
      this.productService.getProduct(id).subscribe(res => {
        this.product = res;
        this.product.comments = [];
        
      });
    }

  }

  rateProduct(rating: number): void {
  }

  addComment(): void {
  }
}
