import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../../shared/model/product';
import { ProductReview } from '../../shared/model/productReview';
import { HttpClient, HttpParams } from '@angular/common/http';
import { PagedList } from '../../shared/model/pagedList';
import { ProductType } from '../../shared/model/productType';
import { ProductBrand } from '../../shared/model/productBrand';
import { environment } from 'src/environments/environment';
import { ErrorDialogComponent } from 'src/app/shared/components/error-dialog/error-dialog.component';
import { MatDialog } from '@angular/material/dialog';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  url = environment.apiUrl;

  constructor(private http: HttpClient, private dialog: MatDialog) { }

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
    const builder = this.http.get<PagedList<Product[]>>(this.url + 'products', {params});
    return builder;
  }

  public getProduct(id: string): Observable<Product> {
    const builder = this.http.get<Product>(this.url + 'products/'+ id);
    return builder;
  }

  public getBrands(name?: string): Observable<ProductBrand[]> {
    const builder = this.http.get<ProductBrand[]>(this.url + 'products/brands');
    return builder;
  }

  public getTypes(name?: string): Observable<ProductType[]> {
    const builder = this.http.get<ProductType[]>(this.url + 'products/types');
    return builder;
  }

  getProductReviews(productId: string): Observable<ProductReview[]> {
    const url = `${this.url}products/reviews/${productId}`;
    return this.http.get<ProductReview[]>(url);
  }

  getProductRatings(productId: string): Observable<number> {
    const url = `${this.url}products/reviews/rating/${productId}`;
    return this.http.get<number>(url);
  }

  getTopRatedProduct(): Observable<Product> {
    const url = `${this.url}products/reviews/top-rated`;
    return this.http.get<Product>(url);
  }

  addProductReview(review: ProductReview): Observable<any> {
    const url = `${this.url}products/reviews`;
    return this.http.post(url, review);
  }

  camelToKebab(camelCase: any): any {
    Object.keys(camelCase).map(x=> x.replace(/([a-z])([A-Z])/g, '$1-$2').toLowerCase());
    return camelCase
  }


  private openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: errorMessage,
    });
  }
}
