import { Component } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';
import { BasketService } from 'src/app/basket/basket.service';
import { BasketItem } from 'src/app/shared/models/basket';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent {
  constructor(
    public basketService: BasketService,
    public accountService: AccountService
  ) {}

  getItemsCount(basket: BasketItem[]) {
    return basket.reduce((sum, item) => sum + item.quantity, 0);
  }
}
