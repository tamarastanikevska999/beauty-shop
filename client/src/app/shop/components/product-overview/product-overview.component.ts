import { BasketService } from './../../../basket/service/basket.service';
import { AccountService } from 'src/app/account/service/account.service';
import { Component, Input, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { Product } from 'src/app/shared/model/product';
import { ActivatedRoute } from '@angular/router';
import { forkJoin } from 'rxjs';
import { RateProductDialogComponent } from './rate-product-dialog/rate-product-dialog.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-product-overview',
  templateUrl: './product-overview.component.html',
  styleUrls: ['./product-overview.component.scss']
})
export class ProductOverviewComponent implements OnInit{
  product: Product;
  newComment: string = '';
  averageRating = 0;

  constructor(
    private dialog: MatDialog,
    private activatedRoute: ActivatedRoute,
    protected productService: ProductService,
    private basketService: BasketService
  ) { }

  ngOnInit(): void {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (id) {
      forkJoin({
        productDetails: this.productService.getProduct(id),
        reviews: this.productService.getProductReviews(id),
        rating: this.productService.getProductRatings(id),
      }).subscribe({
        next: ({ productDetails, reviews, rating }) => {
          this.product = productDetails;
          this.product.comments = reviews;
          this.averageRating = rating;
        },
        error: (error) => {
          console.error('Error fetching product details:', error);
        }
      });
    }
  }

  rateProduct(rating: number): void {
  }

  addComment(): void {
    const dialogRef = this.dialog.open(RateProductDialogComponent, {
      width: '250px',
      data: { rating: 0 },
    });
  
    dialogRef.afterClosed().subscribe((result) => {
      if (result !== undefined) {
        this.productService
          .addProductReview({
            productId: this.product.id,
            userEmail: localStorage.getItem('username'),
            comment: this.newComment,
            rating: result,
          })
          .subscribe(() => {
            this.newComment = ''; 
            this.ngOnInit();
          });
      }
    });
  }

  addToCart(product: any) {
    const item = {
      basketId: this.basketService.getBasket().id,
      productId: product.id,
      productName: product.name,
      price: product.price,
      quantity: 0,
      pictureUrl: product.pictureUrl,
      brand: product.productBrand,
      type: product.productType
    }
    this.basketService.addItemToBasket(item);
    
  }
}
