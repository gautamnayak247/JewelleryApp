import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EstimateComponent } from './component/estimate.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [EstimateComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class EstimateModule { }
