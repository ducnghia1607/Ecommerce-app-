import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'client';

  constructor(private basketService: BasketService) {}
  ngOnInit(): void {
    const basket_id = localStorage.getItem('basket_id');
    basket_id && this.basketService.getBasket(basket_id);
  }
}
