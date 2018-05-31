import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ViewComponent } from './view/view.component';
import { CreateModalComponent } from './create-modal/create-modal.component';
import { FormsModule } from '@angular/forms';
import { CoreModule } from '../core/core.module';
import { AlertModule, TooltipModule, ModalModule } from 'ngx-bootstrap';
import { StaffRoutes } from './routes';
import { RouterModule } from '@angular/router';
import { TreeModule } from 'ng2-tree';
import { FileUploadModule } from 'ng2-file-upload';
import { StaffService } from './staff.service';
import {
  DxTreeListModule, DxAutocompleteModule, DxTextBoxModule, DxValidatorModule,
  DxCheckBoxModule, DxSelectBoxModule, DxNumberBoxModule, DxFormModule,
  DxTreeViewModule, DxDropDownBoxModule
} from 'devextreme-angular';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    CoreModule,
    AlertModule,

    TooltipModule,
    TreeModule,
    FileUploadModule,
    ModalModule.forRoot(),
    RouterModule.forChild(StaffRoutes),

    DxTreeListModule, DxAutocompleteModule, DxSelectBoxModule, DxNumberBoxModule,
    DxCheckBoxModule, DxValidatorModule, DxTextBoxModule, DxFormModule,
    DxTreeViewModule, DxDropDownBoxModule
  ],
  declarations: [ViewComponent, CreateModalComponent],
  providers: [StaffService],
  exports: [RouterModule],
  entryComponents: [
    CreateModalComponent
  ]
})
export class StaffModule { }
