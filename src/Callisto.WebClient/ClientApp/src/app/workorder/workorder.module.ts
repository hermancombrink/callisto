import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WorkListComponent } from './work-list/work-list.component';
import { NewWorkComponent } from './new-work/new-work.component';
import { CloseWorkComponent } from './close-work/close-work.component';
import { WorkNewComponent } from './work-new/work-new.component';
import { WorkCloseComponent } from './work-close/work-close.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [WorkListComponent, NewWorkComponent, CloseWorkComponent, WorkNewComponent, WorkCloseComponent]
})
export class WorkorderModule { }
