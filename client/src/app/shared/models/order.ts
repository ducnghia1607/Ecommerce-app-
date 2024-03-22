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

export interface OrderToReturn {
  id: number;
  buyerEmail: string;
  orderItems: OrderItemToReturn[];
  deliveryMethod: string;
  shippingPrice: number;
  shipToAddress: Address;
  orderDate: string;
  orderStatus: string;
  subtotal: number;
}

export interface OrderItemToReturn {
  productId: number;
  productName: string;
  pictureUrl: string;
  price: number;
  quantity: number;
}
