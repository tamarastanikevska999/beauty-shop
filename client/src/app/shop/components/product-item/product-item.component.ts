import { Component, Input } from '@angular/core';
import { BasketService } from 'src/app/basket/service/basket.service';
import { Product } from 'src/app/shared/model/product';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent {
  @Input() product?: Product;
  constructor(private basketService: BasketService) {}

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
