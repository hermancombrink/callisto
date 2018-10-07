import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DetailsComponent } from './details/details.component';
import { ViewComponent } from './view/view.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [DetailsComponent, ViewComponent]
})
export class WorkorderModule { }
