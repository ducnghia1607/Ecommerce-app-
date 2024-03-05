import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Product } from './models/Product';
import { Pagination } from './models/Pagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'client';
  products: Product[] = [];
  constructor(private http: HttpClient) {}
  ngOnInit(): void {
    this.GetProducts();
  }

  GetProducts() {
    return this.http
      .get<Pagination<Product[]>>('https://localhost:5001/api/products')
      .subscribe({
        next: (res: Pagination<Product[]>) => {
          this.products = res.data;
        },
        error: (err: any) => console.log(err),
        complete: () => console.log('Completed'),
      });
  }
}
