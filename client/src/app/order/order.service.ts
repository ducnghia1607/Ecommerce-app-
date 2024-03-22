import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Order, OrderToReturn } from '../shared/models/order';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getOrders() {
    return this.http.get<OrderToReturn[]>(this.baseUrl + 'order');
  }

  getOrderById(id: number) {
    return this.http.get<OrderToReturn>(this.baseUrl + 'order/' + id);
  }
}
