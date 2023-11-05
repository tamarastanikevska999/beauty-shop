import { Product } from './../../model/product';
import { ProductService } from './../../services/product.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.scss']
})
export class ProductsListComponent implements OnInit {

  products: Product[] = [];
  constructor(
    protected productService: ProductService
  ) { }

  ngOnInit(): void {
    this.productService.getProducts().subscribe(res => {
      console.log('products',res);
      this.products = res.products;
    })
  }

}
