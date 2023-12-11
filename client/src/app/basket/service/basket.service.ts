import { Product } from 'src/app/shared/model/product';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Basket, ProductInBasket } from 'src/app/shared/model/basket';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  url = environment.apiUrl + 'basket';
  private basketSource = new BehaviorSubject<Basket | null>(null);
  basketSource$ = this.basketSource.asObservable();
  // private basketTotalSource = new BehaviorSubject<BasketTotals | null>(null);
  // basketTotalSource$ = this.basketTotalSource.asObservable();

  constructor(private http: HttpClient) { }

  getBasket(): Basket {
    const data = localStorage.getItem('basket');
    return JSON.parse(data);
  }

  getCustomersBasket(email: string): Observable<Basket> {
    return this.http.get<Basket>(`${this.url}/user/${email}`).pipe(
      tap((basket) => {
        localStorage.setItem('basket',JSON.stringify(basket));
        this.basketSource.next(basket);
      })
    );
  }

  public updateBasket(basket: Basket){
    console.log('In apdate', basket);
    const builder = this.http.put<Basket>(this.url, basket).subscribe({
      next: basket => {
        localStorage.setItem('basket',JSON.stringify(basket));
        this.basketSource.next(basket);
      }
    })
  }

  addItemToBasket(item: any, quantity = 1) {
    const basket = this.getBasket();
    console.log('ADD', basket);
    if (!basket) return;
    basket.items = this.addOrUpdateItem(basket.items, item, quantity);
    this.updateBasket(basket);
  }

  removeItemFromBasket(id: number, quantity = 1) {
    const basket = this.getBasket();
    if (!basket) return;
    const item = basket.items.find(x => x.productId == id);
    console.log('test item', item);
    if (item) {
      item.quantity -= quantity;
      if (item.quantity === 0) {
        basket.items = basket.items.filter(x => x.productId != id);
        console.log('test', item.quantity, basket.items);
      }
      this.updateBasket(basket);
    }
  }

  private addOrUpdateItem(items: ProductInBasket[], itemToAdd: ProductInBasket, quantity: number): ProductInBasket[] {
    const item = items.find(x => x.productId == itemToAdd.productId);
    console.log('ADD item',itemToAdd, item);
    if (item) item.quantity += quantity;
    else {
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    }
    console.log('ADD items', items);
    return items;
  }

}
