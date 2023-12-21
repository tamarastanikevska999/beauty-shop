import { BasketTotals } from './../../shared/model/basket';
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
  shipping = 150;
  url = environment.apiUrl + 'basket';
  private basketSource = new BehaviorSubject<Basket | null>(null);
  basketSource$ = this.basketSource.asObservable();
  private basketTotalSource = new BehaviorSubject<BasketTotals | null>(null);
  basketTotalSource$ = this.basketTotalSource.asObservable();

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
        this.calculateTotals();
      })
    );
  }

  public updateBasket(basket: Basket){
    const builder = this.http.put<Basket>(this.url, basket).subscribe({
      next: basket => {
        localStorage.setItem('basket',JSON.stringify(basket));
        this.basketSource.next(basket);
        this.calculateTotals();
      }
    })
  }
  emptyBasket(){
    const basket = this.getBasket();
    const url = `${this.url}/${basket.userEmail}/}/empty`;
    const builder=  this.http.delete<any>(url).subscribe({
      next: basket => {
        localStorage.setItem('basket',JSON.stringify(basket));
        this.basketSource.next(basket);
        this.calculateTotals();
      }
    });
  }

  deleteBasketItem(productId: number){
    const basket = this.getBasket();
    const url = `${this.url}/${basket.userEmail}/delete/${productId}`;
    const builder=  this.http.delete<any>(url).subscribe({
      next: basket => {
        localStorage.setItem('basket',JSON.stringify(basket));
        this.basketSource.next(basket);
        this.calculateTotals();
      }
    });
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
    if (item) {
      item.quantity -= quantity;
      if (item.quantity === 0) {
        basket.items = basket.items.filter(x => x.productId != id);
      }
      this.updateBasket(basket);
    }
  }

  private addOrUpdateItem(items: ProductInBasket[], itemToAdd: ProductInBasket, quantity: number): ProductInBasket[] {
    const item = items.find(x => x.productId == itemToAdd.productId);
    if (item) item.quantity += quantity;
    else {
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    }
    return items;
  }

  private calculateTotals() {
    const basket = this.getBasket();
    if (!basket) return;
    
    const subtotal = basket.items.reduce((a, b) => (b.price * b.quantity) + a, 0);
    const shipping = subtotal > 1000 ? 0 : this.shipping;
    const total = subtotal + shipping;
    this.basketTotalSource.next({ shipping, total, subtotal});
  }

}
