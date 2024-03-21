import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account/account.service';
import { Address } from '../shared/models/User';
import { ToastrService } from 'ngx-toastr';
import { BasketService } from '../basket/basket.service';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss'],
})
export class CheckoutComponent {
  constructor(
    private fb: FormBuilder,
    private accountService: AccountService,
    private basketService: BasketService
  ) {
    this.loadCurrentAddress();
    this.loadCurrentDelivery();
  }
  checkoutForm: FormGroup = this.fb.group({
    addressForm: this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      street: ['', Validators.required],
      city: ['', Validators.required],
      state: ['', Validators.required],
      zipcode: ['', Validators.required],
    }),
    deliveryForm: this.fb.group({
      deliveryMethod: ['', Validators.required],
    }),
    paymentForm: this.fb.group({
      nameOnCard: ['', Validators.required],
    }),
  });

  loadCurrentAddress() {
    this.accountService.getUserAddress().subscribe({
      next: (address: Address) => {
        this.checkoutForm?.get('addressForm')?.patchValue(address);
      },
    });
  }

  loadCurrentDelivery() {
    var basket = this.basketService.getCurrentBasketValue();
    if (basket?.deliveryMethodId) {
      this.checkoutForm
        .get('deliveryForm')
        ?.get('deliveryMethod')
        ?.patchValue(basket.deliveryMethodId.toString());
    }
  }
}
