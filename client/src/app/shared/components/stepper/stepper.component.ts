import { CdkStepper } from '@angular/cdk/stepper';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-stepper',
  templateUrl: './stepper.component.html',
  styleUrls: ['./stepper.component.scss'],
  providers: [{ provide: CdkStepper, useExisting: StepperComponent }],
})
export class StepperComponent extends CdkStepper implements OnInit {
  @Input() linearModelSelected: boolean = true;

  ngOnInit(): void {
    // linear = false : khong check form hoan thanh co the chuyen qua lai giua cac form
    this.linear = this.linearModelSelected;
  }
  selectStepByIndex(index: number): void {
    this.selectedIndex = index;
  }
}
