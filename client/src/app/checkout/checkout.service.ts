import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { DeliveryMethod } from '../shared/models/delivery';
import { OrderToCreate } from '../shared/models/order';

@Injectable({
  providedIn: 'root',
})
export class CheckoutService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  getDeliveryMethods() {
    return this.http.get<DeliveryMethod[]>(
      this.baseUrl + 'order/deliveryMethods'
    );
  }

  createOrder(order: OrderToCreate) {
    return this.http.post<OrderToCreate>(this.baseUrl + 'order', order);
  }
}
