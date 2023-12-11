import { BasketService } from 'src/app/basket/service/basket.service';
import { Component } from '@angular/core';
import { AccountService } from 'src/app/account/service/account.service';
import { ProductInBasket } from 'src/app/shared/model/basket';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent {
  
  constructor(public accountService: AccountService, public basketService: BasketService) {}

  getCount(items: ProductInBasket[]) {
    return items.reduce((sum, item) => sum + item.quantity, 0);
  }

}
