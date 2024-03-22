import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.scss'],
})
export class PagerComponent {
  @Input() totalItems?: number;
  @Input() pageSize?: number;
  @Input() pageNumber?: number;
  // Vì emit ra event.page nên để kiểu EventEmitter là number
  @Output() pagerChanged = new EventEmitter<number>();

  onPagerChanged(event: any) {
    this.pagerChanged.emit(event.page);
  }
}
