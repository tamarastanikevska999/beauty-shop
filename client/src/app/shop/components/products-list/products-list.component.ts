import { error } from 'console';
import { Product } from '../../model/product';
import { ProductService } from '../../services/product.service';
import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ProductBrand } from '../../model/productBrand';
import { ProductType } from '../../model/productType';

@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.scss']
})
export class ProductsListComponent implements OnInit, AfterViewInit {
  public formGroup: FormGroup;
  // priceRange: number = 500; // Default value
  minPrice: number = 0;
  maxPrice: number = 1000;
  products: Product[] = [];
  brands: ProductBrand[] = [];
  types: ProductType[] = [];
  filters = {
    "sort-by": "name",
    "sort-order": "desc",
    "type": null,
    "brand": null
  };
  sortOptions = [
    {name: 'Alphabetical', sortBy: 'name',sortOrder: 'desc'},
    {name: 'Price: Low to high', sortBy: 'price',sortOrder: 'asc'},
    {name: 'Price: High to low', sortBy: 'price',sortOrder: 'desc'},
  ];
  constructor(
    protected productService: ProductService
  ) { }
  ngAfterViewInit(): void {

  }

  ngOnInit(): void {
    this.initForm();
    this.getProducts();
    this.getBrands();
    this.getTypes();
    
  }

  private getProducts(){
    this.productService.getProducts(this.filters).subscribe(res => {
      console.log('products',res);
      this.products = res.items;
    }, error => console.log(error))
  }

  private getBrands(){
    this.productService.getBrands().subscribe(res => {
      console.log('brands',res);
      this.brands = res;
    }, error => console.log(error))
  }

  private getTypes(){
    this.productService.getTypes().subscribe(res => {
      console.log('types',res);
      this.types = res;
    }, error => console.log(error))
  }

  private initForm() {
    this.formGroup = new FormGroup({
      brand: new FormControl(''),
      type: new FormControl(''),
      sort: new FormControl(this.sortOptions[0]),
      // priceRange: new FormControl(500),
    });
    this.formGroup.controls.sort.valueChanges.subscribe(res =>
      {
        console.log(res);
        this.filters["sort-by"] = res.sortBy;
        this.filters["sort-order"] = res.sortOrder;
        this.getProducts();
      })
    this.formGroup.controls.brand.valueChanges.subscribe(res =>
        {
          console.log(res);
          this.filters.brand = res;
          this.getProducts();
    })

    this.formGroup.controls.type.valueChanges.subscribe(res =>
      {
        console.log(res);
        this.filters.type = res;
        this.getProducts();
  })
  }

  resetSelection(): void {
    this.formGroup.controls.brand.setValue('');
    this.formGroup.controls.type.setValue('');
    this.formGroup.controls.sort.setValue(this.sortOptions[0]);
  }

}
