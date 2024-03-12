import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss'],
})
export class TestErrorComponent {
  baseUrl = environment.apiUrl;
  errors: string[] = [];
  constructor(private http: HttpClient) {}
  get404Error() {
    this.http.get(this.baseUrl + 'products/100').subscribe({
      next: (res) => console.log(res),
      error: (err) => console.log(err),
    });
  }

  get400Error() {
    this.http.get(this.baseUrl + 'buggy/badrequest').subscribe({
      next: (res) => console.log(res),
      error: (err) => console.log(err),
    });
  }

  getValidation400Error() {
    this.http.get(this.baseUrl + 'products/abc').subscribe({
      next: (res) => console.log(res),
      error: (err) => {
        console.log(err);
        this.errors = err.errors;
      },
    });
  }

  getServerError() {
    this.http.get(this.baseUrl + 'buggy/servererror').subscribe({
      next: (res) => console.log(res),
      error: (err) => console.log(err),
    });
  }
}
