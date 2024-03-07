import { Component, OnInit } from '@angular/core';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { Product } from 'src/app/shared/models/Product';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit {
  product?: Product;
  constructor(
    private shopService: ShopService,
    private activetedRoute: ActivatedRoute,
    private bsService: BreadcrumbService
  ) {
    this.bsService.set('@productDetails', ' ');
    this.getProduct();
  }
  ngOnInit(): void {}

  getProduct() {
    const id = this.activetedRoute.snapshot.paramMap.get('id');
    if (id)
      this.shopService.getProduct(+id).subscribe({
        next: (res) => {
          this.product = res;
          this.bsService.set('@productDetails', this.product.name);
        },
        error: (err) => console.log(err),
      });
  }
}
