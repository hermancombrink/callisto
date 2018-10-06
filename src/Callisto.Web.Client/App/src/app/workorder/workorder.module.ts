import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DetailsComponent } from './details/details.component';
import { ViewComponent } from './view/view.component';
import { CreateModalComponent } from './create-modal/create-modal.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [DetailsComponent, ViewComponent, CreateModalComponent]
})
export class WorkorderModule { }
