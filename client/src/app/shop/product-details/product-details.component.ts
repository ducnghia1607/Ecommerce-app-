import { Component, OnInit } from '@angular/core';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { Product } from 'src/app/shared/models/Product';
import { BreadcrumbService } from 'xng-breadcrumb';
import { BasketService } from 'src/app/basket/basket.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit {
  product?: Product;

  quantity: number = 0;
  quantityInBasket: number = 0;

  constructor(
    private shopService: ShopService,
    private activetedRoute: ActivatedRoute,
    private bsService: BreadcrumbService,
    private basketService: BasketService
  ) {
    this.bsService.set('@productDetails', ' ');
    this.getProduct();
  }
  ngOnInit(): void {}

  getProduct() {
    const id = this.activetedRoute.snapshot.paramMap.get('id');
    if (id)
      this.shopService.getProduct(+id).subscribe({
        next: (res) => {
          this.product = res;
          this.bsService.set('@productDetails', this.product.name);
          this.basketService.basketSource$.pipe(take(1)).subscribe({
            next: (basket) => {
              const item = basket?.items.find((i) => i.id === +id);
              if (item) {
                this.quantity = item.quantity;
                this.quantityInBasket = item.quantity;
              }
            },
          });
        },
        error: (err) => console.log(err),
      });
  }

  incrementQuantity() {
    this.quantity++;
  }

  decrementQuantity() {
    this.quantity--;
  }

  updateBasket(item: Product) {
    if (this.quantity > this.quantityInBasket) {
      const itemsToAdd = this.quantity - this.quantityInBasket;
      this.basketService.AddItemToBasket(item, itemsToAdd);
    } else {
      const itemsToRemove = this.quantityInBasket - this.quantity;
      this.basketService.RemoveItemBasket(item.id, itemsToRemove);
    }

    this.quantityInBasket = this.quantity;
  }

  get buttonText() {
    return this.quantityInBasket == 0 ? 'Add to basket' : 'Update to basket';
  }
}
