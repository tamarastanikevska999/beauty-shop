import { ProductService } from './../../../shop/services/product.service';
import { Product } from './../../../shared/model/product';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home-overview',
  templateUrl: './home-overview.component.html',
  styleUrls: ['./home-overview.component.scss']
})
export class HomeOverviewComponent {
  topRatedProduct: Product | null = null;
  topRatedRating = 0;
  images = [
    { src: 'assets/carousel_2.jpg', alt: 'Second slide', label: 'SHOP NOW', content: 'Science Meets Beauty - Results You Can See' },
    { src: 'assets/carousel_3.jpg', alt: 'Third slide', label: 'SHOP NOW', content: 'Indulge in Self-Care, Unleash Confidence' },
    { src: 'assets/carousel_1.jpg', alt: 'First slide', label: 'SHOP NOW', content: 'Radiant, Healthy Skin Awaits You' },
  ];

  activeIndex = 0;
  constructor(private productService: ProductService, private router: Router) {}

  ngOnInit(): void {
    setInterval(() => {
      this.nextSlide();
    }, 5000);
    this.productService.getTopRatedProduct().subscribe(
      (product) => {
        this.topRatedProduct = product;
        return this.productService.getProductRatings(this.topRatedProduct.id).subscribe(res => this.topRatedRating = res);
      },
      (error) => {
        console.error('Error fetching top-rated product:', error);
      }
    );
  }

  navigateToProduct(): void {
    this.router.navigate(['/products', this.topRatedProduct.id]);
  }

  setIndex(index: number) {
    this.activeIndex = index;
  }

  prevSlide() {
    this.activeIndex = (this.activeIndex - 1 + this.images.length) % this.images.length;
  }

  nextSlide() {
    this.activeIndex = (this.activeIndex + 1) % this.images.length;
  }
}
