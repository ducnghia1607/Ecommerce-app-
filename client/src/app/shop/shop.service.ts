import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pagination } from '../shared/models/Pagination';
import { Product } from '../shared/models/Product';
import { Brand } from '../shared/models/Brand';
import { Type } from '../shared/models/Type';
import { ProductParams } from '../shared/models/ProductParams';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) {}

  getProducts(productParams: ProductParams) {
    let params = new HttpParams();

    if (productParams.brandId > 0)
      params = params.append('brandId', productParams.brandId);
    if (productParams.typeId > 0)
      params = params.append('typeId', productParams.typeId);
    if (productParams.sort) params = params.append('sort', productParams.sort);

    params = params.append('pageIndex', productParams.pageIndex);
    params = params.append('pageSize', productParams.pageSize);
    if (productParams.search)
      params = params.append('search', productParams.search);
    return this.http.get<Pagination<Product[]>>(this.baseUrl + 'products', {
      params: params,
    });
  }

  getBrands() {
    return this.http.get<Brand[]>(this.baseUrl + 'products/brands');
  }

  getTypes() {
    return this.http.get<Type[]>(this.baseUrl + 'products/types');
  }
}
