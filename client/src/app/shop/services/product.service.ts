import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../model/product';
import { HttpClient } from '@angular/common/http';
import { PagedList } from '../model/pagedList';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  url = 'https://localhost:5001'

  constructor(private http: HttpClient) { }

  public getProducts(filters?: any): Observable<PagedList<Product[]>> {
    const builder = this.http.get<PagedList<Product[]>>(this.url + '/products');
    return builder;
  }
}
