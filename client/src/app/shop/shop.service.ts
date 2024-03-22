import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, TypeProvider } from '@angular/core';
import { Pagination } from '../shared/models/Pagination';
import { Product } from '../shared/models/Product';
import { Brand } from '../shared/models/Brand';
import { Type } from '../shared/models/Type';
import { ProductParams } from '../shared/models/ProductParams';
import { environment } from 'src/environments/environment';
import { Observable, map, of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = environment.apiUrl;
  products: Product[] = [];
  brands: Brand[] = [];
  types: Type[] = [];
  pagination?: Pagination<Product[]>;
  productParams: ProductParams = new ProductParams();
  productCache = new Map<string, Pagination<Product[]>>();
  constructor(private http: HttpClient) {}

  getProducts(useCache = true): Observable<Pagination<Product[]>> {
    // console.log(this.productCache);

    if (!useCache) this.productCache = new Map();
    if (this.productCache.size > 0 && useCache) {
      if (this.productCache.has(Object.values(this.productParams).join('-'))) {
        this.pagination = this.productCache.get(
          Object.values(this.productParams).join('-')
        );
        if (this.pagination) return of(this.pagination);
      }
    }

    let params = new HttpParams();
    if (this.productParams.brandId > 0)
      params = params.append('brandId', this.productParams.brandId);
    if (this.productParams.typeId > 0)
      params = params.append('typeId', this.productParams.typeId);
    if (this.productParams.sort)
      params = params.append('sort', this.productParams.sort);

    params = params.append('pageIndex', this.productParams.pageIndex);
    params = params.append('pageSize', this.productParams.pageSize);
    if (this.productParams.search)
      params = params.append('search', this.productParams.search);
    return this.http
      .get<Pagination<Product[]>>(this.baseUrl + 'products', {
        params: params,
      })
      .pipe(
        map((response) => {
          // this.products = [...this.products, ...response.data];
          this.productCache.set(
            Object.values(this.productParams).join('-'),
            response
          );
          this.pagination = response;
          console.log(this.productCache);

          return response;
        })
      );
  }

  getProductParams() {
    return this.productParams;
  }

  setProductParams(params: ProductParams) {
    this.productParams = params;
  }

  getProduct(id: number) {
    const product = [...this.productCache.values()].reduce(
      (acc, pagedResult) => {
        return { ...acc, ...pagedResult.data.find((x) => x.id === id) };
      },
      {} as Product
    );

    if (Object.keys.length !== 0) return of(product);

    // if (product) return of(product);
    return this.http.get<Product>(this.baseUrl + 'products/' + id);
  }

  getBrands() {
    if (this.brands.length > 0) return of(this.brands);
    return this.http.get<Brand[]>(this.baseUrl + 'products/brands').pipe(
      map((response) => {
        this.brands = response;
        return response;
      })
    );
  }

  getTypes() {
    if (this.types.length > 0) return of(this.types);
    return this.http.get<Type[]>(this.baseUrl + 'products/types').pipe(
      map((response) => {
        this.types = response;
        return response;
      })
    );
  }
}
