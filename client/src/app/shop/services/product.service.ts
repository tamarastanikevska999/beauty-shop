import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../model/product';
import { HttpClient, HttpParams } from '@angular/common/http';
import { PagedList } from '../model/pagedList';
import { ProductType } from '../model/productType';
import { ProductBrand } from '../model/productBrand';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  url = 'https://localhost:5001'

  constructor(private http: HttpClient) { }

  public getProducts(filters?: any): Observable<PagedList<Product[]>> {
    for (const key in filters) {
      if (filters.hasOwnProperty(key) && !filters[key]) {
        delete filters[key];
      }
    }
    var params = new HttpParams();
    if(filters){
      params = filters;
    }
    const builder = this.http.get<PagedList<Product[]>>(this.url + '/products', {params});
    return builder;
  }

  public getProduct(id: number): Observable<Product> {
    const builder = this.http.get<Product>(this.url + '/products/'+ id);
    return builder;
  }

  public getBrands(name?: string): Observable<ProductBrand[]> {
    const builder = this.http.get<ProductBrand[]>(this.url + '/products/brands');
    return builder;
  }

  public getTypes(name?: string): Observable<ProductType[]> {
    const builder = this.http.get<ProductType[]>(this.url + '/products/types');
    return builder;
  }

  camelToKebab(camelCase: any): any {
    Object.keys(camelCase).map(x=> x.replace(/([a-z])([A-Z])/g, '$1-$2').toLowerCase());
    return camelCase
  }
}
