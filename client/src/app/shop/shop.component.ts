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
  productParams: ProductParams = new ProductParams();
  totalItems = 0;
  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price:Low to high', value: 'priceAsc' },
    { name: 'Price:High to low', value: 'priceDesc' },
  ];

  constructor(private shopService: ShopService) {}
  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts() {
    this.shopService.getProducts(this.productParams).subscribe({
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
    this.productParams.brandId = brandId;
    this.getProducts();
  }

  onTypeSelected(typeId: number) {
    this.productParams.typeId = typeId;
    this.productParams.pageIndex = 1;
    this.getProducts();
  }

  onSortSelected(event: any) {
    this.productParams.sort = event.target.value;
    this.productParams.pageIndex = 1;

    this.getProducts();
  }

  onPageChanged(event: any) {
    if (this.productParams.pageIndex != event) {
      this.productParams.pageIndex = event;
      this.getProducts();
    }
  }

  onSearch() {
    if (this.searchTerm?.nativeElement.value) {
      this.productParams.search = this.searchTerm.nativeElement.value;
      this.productParams.pageIndex = 1;
      this.getProducts();
    }
  }

  onReset() {
    if (this.searchTerm?.nativeElement.value) {
      this.searchTerm.nativeElement.value = '';
    }
    this.productParams = new ProductParams();
    this.getProducts();
  }
}
