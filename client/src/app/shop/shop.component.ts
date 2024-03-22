import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ShopService } from './shop.service';
import { Product } from '../shared/models/Product';
import { Type } from '../shared/models/Type';
import { Brand } from '../shared/models/Brand';
import { ProductParams } from '../shared/models/ProductParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  @ViewChild('search') searchTerm?: ElementRef;
  products: Product[] = [];
  brands: Brand[] = [];
  types: Type[] = [];
  productParams: ProductParams;
  totalItems = 0;
  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price:Low to high', value: 'priceAsc' },
    { name: 'Price:High to low', value: 'priceDesc' },
  ];

  constructor(private shopService: ShopService) {
    this.productParams = this.shopService.getProductParams();
  }
  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts() {
    this.shopService.getProducts().subscribe({
      next: (res) => {
        this.products = res.data;
        this.totalItems = res.count;
      },
      error: (error) => console.log(error),
    });
  }

  getBrands() {
    this.shopService.getBrands().subscribe({
      next: (res) => {
        this.brands = [{ id: 0, name: 'All' }, ...res];
      },
      error: (error) => console.log(error),
    });
  }

  getTypes() {
    this.shopService.getTypes().subscribe({
      next: (res) => {
        this.types = [{ id: 0, name: 'All' }, ...res];
      },
      error: (error) => console.log(error),
    });
  }

  onBrandSelected(brandId: number) {
    var params = this.shopService.getProductParams();
    params.brandId = brandId;
    this.shopService.setProductParams(params);
    this.productParams = params;
    this.getProducts();
  }

  onTypeSelected(typeId: number) {
    var params = this.shopService.getProductParams();
    params.typeId = typeId;
    params.pageIndex = 1;
    this.shopService.setProductParams(params);
    this.productParams = params;
    this.getProducts();
  }

  onSortSelected(event: any) {
    var params = this.shopService.getProductParams();
    params.sort = event.target.value;
    params.pageIndex = 1;
    this.shopService.setProductParams(params);
    this.productParams = params;

    this.getProducts();
  }

  onPageChanged(event: any) {
    var params = this.shopService.getProductParams();
    if (params.pageIndex != event) {
      params.pageIndex = event;
      this.shopService.setProductParams(params);
      this.productParams = params;
      this.getProducts();
    }
  }

  onSearch() {
    var params = this.shopService.getProductParams();
    if (this.searchTerm?.nativeElement.value) {
      params.search = this.searchTerm.nativeElement.value;
      params.pageIndex = 1;
      this.shopService.setProductParams(params);
      this.productParams = params;

      this.getProducts();
    }
  }

  onReset() {
    if (this.searchTerm?.nativeElement.value) {
      this.searchTerm.nativeElement.value = '';
    }
    var params = new ProductParams();
    this.shopService.setProductParams(params);
    this.productParams = params;
    this.getProducts();
  }
}
