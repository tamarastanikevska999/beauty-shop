import { AccountService } from 'src/app/account/service/account.service';
import { Component } from '@angular/core';
import { ProductInBasket } from 'src/app/shared/model/basket';
import { BasketService } from '../../service/basket.service';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent {

  constructor(public basketService: BasketService, public accountService: AccountService) {
  }

  incrementQuantity(item: ProductInBasket) {
    this.basketService.addItemToBasket(item);
  }

  removeItem(id: number, quantity: number) {
    this.basketService.removeItemFromBasket(id, quantity);
  }

  deleteItem(id: number) {
    console.log(id);
    this.basketService.deleteBasketItem(id);
  }

}
