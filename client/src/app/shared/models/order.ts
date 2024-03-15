import { Address } from './User';
import { DeliveryMethod } from './delivery';

export interface OrderToCreate {
  basketId: string;
  deliveryMethodId: number;
  shipToAddress: Address;
}

export interface Order {
  buyerEmail: string;
  orderItems: OrderItem[];
  deliveryMethod: DeliveryMethod;
  shipToAddress: Address;
  orderDate: string;
  orderStatus: number;
  subtotal: number;
  paymentIntentId: any;
  id: number;
}

export interface OrderItem {
  itemOrdered: ItemOrdered;
  price: number;
  quantity: number;
  id: number;
}

export interface ItemOrdered {
  productItemId: number;
  productName: string;
  pictureUrl: string;
}
