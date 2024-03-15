import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/account/account.service';
import { Address } from 'src/app/shared/models/User';

@Component({
  selector: 'app-checkout-address',
  templateUrl: './checkout-address.component.html',
  styleUrls: ['./checkout-address.component.scss'],
})
export class CheckoutAddressComponent implements OnInit {
  @Input() checkoutForm?: FormGroup;
  constructor(
    private accountService: AccountService,
    private toastService: ToastrService
  ) {}
  ngOnInit(): void {
    this.getUserAddress();
  }
  getUserAddress() {
    this.accountService.getUserAddress().subscribe({
      next: (address: Address) => {
        this.checkoutForm?.get('addressForm')?.patchValue(address);
      },
    });
  }

  saveUserAddress() {
    this.accountService
      .updateUserAddress(this.checkoutForm?.get('addressForm')?.value)
      .subscribe({
        next: () => {
          // this.checkoutForm.get('addressForm')?.patchValue()
          this.toastService.success('Address saved ');
          this.checkoutForm
            ?.get('addressForm')
            ?.reset(this.checkoutForm?.get('addressForm')?.value);
        },
      });
  }
}
