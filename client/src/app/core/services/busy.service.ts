import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root',
})
export class BusyService {
  busyCountRequest = 0;
  constructor(private spinner: NgxSpinnerService) {}

  busy() {
    this.busyCountRequest++;
    this.spinner.show();
  }

  idle() {
    this.busyCountRequest--;
    if (this.busyCountRequest <= 0) {
      this.busyCountRequest = 0;
      this.spinner.hide();
    }
  }
}
