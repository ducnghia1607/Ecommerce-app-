import { Component, Input } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { Product } from 'src/app/shared/models/Product';
import { BasketItem } from 'src/app/shared/models/basket';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss'],
})
export class ProductItemComponent {
  @Input() product!: Product;

  constructor(private basketService: BasketService) {}

  onAddToCart(item: Product) {
    this.basketService.AddItemToBasket(item);
  }
}
