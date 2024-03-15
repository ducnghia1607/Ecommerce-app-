import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

import { Basket, BasketItem, BasketTotalCost } from '../shared/models/basket';
import { Product } from '../shared/models/Product';
import { environment } from 'src/environments/environment';
import { DeliveryMethod } from '../shared/models/delivery';

@Injectable({
  providedIn: 'root',
})
export class BasketService {
  baseUrl = environment.apiUrl;
  private basketSource = new BehaviorSubject<Basket | null>(null);
  basketSource$ = this.basketSource.asObservable();
  private basketTotalCostSource = new BehaviorSubject<BasketTotalCost | null>(
    null
  );
  shipping = 0;

  basketTotalCostSource$ = this.basketTotalCostSource.asObservable();

  constructor(private http: HttpClient) {}

  getBasket(id: string) {
    return this.http
      .get<Basket>(this.baseUrl + 'basket?id=' + id)
      .subscribe((basket) => {
        this.basketSource.next(basket);
        this.calculateTotalCost();
      });
  }

  getShippingPrice(deliveryMethod: DeliveryMethod) {
    this.shipping = deliveryMethod.price;
    this.calculateTotalCost();
  }

  setBasket(basket: Basket) {
    return this.http
      .post<Basket>(this.baseUrl + 'basket', basket)
      .subscribe((basket: Basket) => {
        this.basketSource.next(basket);
        this.calculateTotalCost();
      });
  }

  getCurrentBasketValue() {
    return this.basketSource.value;
  }

  AddItemToBasket(item: Product | BasketItem, quantity = 1) {
    if (this.isProduct(item)) item = this.mapProductItemToBasketItem(item);
    const basket = this.getCurrentBasketValue() ?? this.createBasket();
    basket.items = this.AddOrUpdateBasket(item, quantity, basket);

    //Dùng setBasket thay vì next để cập nhật basket trên server
    this.setBasket(basket);
  }

  RemoveItemBasket(id: number, quantity = 1) {
    const basket = this.getCurrentBasketValue();
    if (!basket) return;
    const item = basket.items.find((i) => i.id === id);
    if (item) {
      item.quantity -= quantity;
      if (item.quantity == 0) {
        basket.items = basket.items.filter((i) => i.id != id);
      }

      if (basket.items.length > 0) {
        this.setBasket(basket);
      } else {
        this.deleteBasket(basket);
      }
    }
  }

  deleteBasket(basket: Basket) {
    return this.http.delete(this.baseUrl + 'basket?id=' + basket.id).subscribe({
      next: () => {
        this.deleteLocalBasket();
      },
    });
  }

  deleteLocalBasket() {
    this.basketSource.next(null);
    this.basketTotalCostSource.next(null);
    localStorage.removeItem('basket_id');
  }
  AddOrUpdateBasket(
    item: BasketItem,
    quantity: number,
    basket: Basket
  ): BasketItem[] {
    var itemToAdd = basket.items.find((i) => i.id == item.id);
    if (itemToAdd) {
      itemToAdd.quantity += quantity;
    } else {
      item.quantity = quantity;
      basket.items.push(item);
    }

    return basket.items;
  }

  private createBasket(): Basket {
    var basket = new Basket();
    localStorage.setItem('basket_id', basket.id);
    return basket;
  }

  private mapProductItemToBasketItem(item: Product): BasketItem {
    return {
      id: item.id,
      productName: item.name,
      price: item.price,
      pictureUrl: item.pictureUrl,
      quantity: 0,
      brand: item.productBrand,
      type: item.productType,
    };
  }

  private calculateTotalCost() {
    const basket = this.getCurrentBasketValue();
    if (!basket) return;
    const subtotal = basket.items.reduce((a, b) => a + b.price * b.quantity, 0);
    const total = this.shipping + subtotal;
    const shipping = this.shipping;
    this.basketTotalCostSource.next({ shipping, subtotal, total });
  }

  private isProduct(item: Product | BasketItem): item is Product {
    return (item as Product).productBrand !== undefined;
  }
}
