import { createId } from '@paralleldrive/cuid2';

export interface BasketItem {
  id: number;
  productName: string;
  price: number;
  quantity: number;
  pictureUrl: string;
  brand: string;
  type: string;
}

export interface Basket {
  id: string;
  items: BasketItem[];
}

export class Basket implements Basket {
  // id = 'aaaddafaffffe12';
  id = createId();
  items: BasketItem[] = [];
  deliveryMethodId?: number;
  clientSecret?: string;
  paymentIntentId?: string;
  shippingPrice = 0;
}

export interface BasketTotalCost {
  shipping: number;
  subtotal: number;
  total: number;
}
