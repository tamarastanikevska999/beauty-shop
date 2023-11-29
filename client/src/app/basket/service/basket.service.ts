import { Product } from 'src/app/shared/model/product';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Basket, ProductInBasket } from 'src/app/shared/model/basket';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  url = environment.apiUrl + 'basket';

  constructor(private http: HttpClient) { }

  public getBasket(id: string): Observable<Basket> {
    const builder = this.http.get<Basket>(this.url + '/'+ id);
    return builder;
  }

  public updateBasket(basket: Basket): Observable<Basket> {
    const builder = this.http.post<Basket>(this.url, basket);
    return builder;
  }

  public addProductInBasket(item: Product, quantity = 1, basket: Basket): Observable<Basket> {
    const builder = this.http.put<Basket>(this.url, basket);
    return builder;
  }

  public deleteProductFromBasket(item: Product, quantity = 1, basket: Basket): Observable<Basket> {
    const builder = this.http.put<Basket>(this.url, basket);
    return builder;
  }

  getCurrentBasketValue() {
    // when identity created
    return '';
  }

}
