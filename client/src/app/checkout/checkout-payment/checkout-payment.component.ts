import { Component, Input } from '@angular/core';
import { CheckoutService } from '../checkout.service';
import { FormGroup } from '@angular/forms';
import { Address } from 'src/app/shared/models/User';
import { BasketService } from 'src/app/basket/basket.service';
import { Basket } from 'src/app/shared/models/basket';
import { OrderToCreate } from 'src/app/shared/models/order';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss'],
})
export class CheckoutPaymentComponent {
  @Input() checkoutForm?: FormGroup;
  constructor(
    private checkoutService: CheckoutService,
    private basketService: BasketService,
    private toastService: ToastrService,
    private router: Router
  ) {}

  submitOrder() {
    const basket = this.basketService.getCurrentBasketValue();
    if (!basket) return;
    const deliveryMethodId = this.checkoutForm
      ?.get('deliveryForm')
      ?.get('deliveryMethodId')?.value;
    const shipToAddress = this.checkoutForm?.get('addressForm')
      ?.value as Address;

    const orderToCreate = this.createOrder(
      basket,
      deliveryMethodId,
      shipToAddress
    );
    if (!orderToCreate) return;
    this.checkoutService.createOrder(orderToCreate).subscribe({
      next: (order) => {
        this.toastService.success('Order created successfully');
        this.router.navigateByUrl('checkout/success');
        this.basketService.deleteLocalBasket();
        console.log(order);
      },
    });
  }

  private createOrder(
    basket: Basket,
    deliveryMethodId: number,
    shipToAddress: Address
  ): OrderToCreate {
    return {
      basketId: basket.id,
      deliveryMethodId: deliveryMethodId,
      shipToAddress: shipToAddress,
    };
  }
}
