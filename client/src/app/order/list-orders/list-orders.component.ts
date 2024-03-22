import { Component } from '@angular/core';
import { Order, OrderToReturn } from 'src/app/shared/models/order';
import { OrderService } from '../order.service';

@Component({
  selector: 'app-list-orders',
  templateUrl: './list-orders.component.html',
  styleUrls: ['./list-orders.component.scss'],
})
export class ListOrdersComponent {
  orders: OrderToReturn[] = [];

  constructor(private orderService: OrderService) {
    this.getOrders();
  }

  getOrders() {
    this.orderService.getOrders().subscribe((res) => {
      this.orders = res;
      console.log(this.orders);
    });
  }
}
