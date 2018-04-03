import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AssetRoutes } from '../asset/routes';
import { WorkOrderRoutes } from './routes';
import { RouterModule } from '@angular/router';
import { WorkCloseComponent } from './work-close/work-close.component';
import { WorkNewComponent } from './work-new/work-new.component';
import { WorkListComponent } from './work-list/work-list.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(WorkOrderRoutes)
  ],
  declarations: [
    WorkCloseComponent,
    WorkNewComponent,
    WorkListComponent
  ]
})
export class WorkorderModule { }
