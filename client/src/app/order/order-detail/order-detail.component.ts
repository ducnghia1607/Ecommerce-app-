import { Component } from '@angular/core';
import { OrderService } from '../order.service';
import { OrderToReturn } from 'src/app/shared/models/order';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-order-detail',
  templateUrl: './order-detail.component.html',
  styleUrls: ['./order-detail.component.scss'],
})
export class OrderDetailComponent {
  order?: OrderToReturn;
  constructor(
    private orderService: OrderService,
    private activatedRoute: ActivatedRoute,
    private bcService: BreadcrumbService
  ) {
    this.getOrder();
  }

  getOrder() {
    var id = this.activatedRoute.snapshot.params['id'];

    id &&
      this.orderService.getOrderById(+id).subscribe((res) => {
        this.order = res;
        this.bcService.set(
          '@OrderDetailed',
          `Order# ${this.order.id} - ${this.order.orderStatus}`
        );
      });
  }
}
