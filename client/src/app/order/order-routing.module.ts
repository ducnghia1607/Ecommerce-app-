import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule, Routes } from '@angular/router';
import { ListOrdersComponent } from './list-orders/list-orders.component';
import { OrderDetailComponent } from './order-detail/order-detail.component';

const routes: Routes = [
  { path: '', component: ListOrdersComponent },
  {
    path: ':id',
    component: OrderDetailComponent,
    data: { breadcrumb: { alias: 'OrderDetailed' } },
  },
];

@NgModule({
  declarations: [],
  imports: [CommonModule, RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class OrderRoutingModule {}
