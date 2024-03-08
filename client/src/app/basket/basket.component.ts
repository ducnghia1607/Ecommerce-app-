import { Component } from '@angular/core';
import { BasketService } from './basket.service';
import { BasketItem } from '../shared/models/basket';
import { Product } from '../shared/models/Product';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss'],
})
export class BasketComponent {
  constructor(public basketService: BasketService) {}

  incrementItem(item: Product | BasketItem) {
    this.basketService.AddItemToBasket(item, 1);
  }

  removeItem(id: number, quantity: number) {
    this.basketService.RemoveItemBasket(id, quantity);
  }
}
