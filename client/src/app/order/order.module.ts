import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderComponent } from './order.component';
import { ListOrdersComponent } from './list-orders/list-orders.component';
import { OrderDetailComponent } from './order-detail/order-detail.component';
import { OrderRoutingModule } from './order-routing.module';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [OrderComponent, ListOrdersComponent, OrderDetailComponent],
  imports: [CommonModule, OrderRoutingModule, HttpClientModule],
})
export class OrderModule {}
