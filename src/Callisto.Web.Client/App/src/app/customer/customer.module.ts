import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ViewComponent } from './view/view.component';
import { CreateModalComponent } from './create-modal/create-modal.component';
import { FormsModule } from '@angular/forms';
import { CoreModule } from '../core/core.module';
import { AlertModule, TooltipModule, ModalModule } from 'ngx-bootstrap';
import { CustomerRoutes } from './routes';
import { RouterModule } from '@angular/router';
import { TreeModule } from 'ng2-tree';
import { FileUploadModule } from 'ng2-file-upload';
import { CustomerService } from './Customer.service';
import { DetailsComponent } from './details/details.component';

import {
  DxTreeListModule, DxAutocompleteModule, DxTextBoxModule, DxValidatorModule,
  DxCheckBoxModule, DxSelectBoxModule, DxNumberBoxModule, DxFormModule,
  DxTreeViewModule, DxDropDownBoxModule, DxDataGridModule, DxRadioGroupModule
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
    RouterModule.forChild(CustomerRoutes),

    DxTreeListModule, DxAutocompleteModule, DxSelectBoxModule, DxNumberBoxModule,
    DxCheckBoxModule, DxValidatorModule, DxTextBoxModule, DxFormModule,
    DxTreeViewModule, DxDropDownBoxModule, DxDataGridModule, DxRadioGroupModule
  ],
  declarations: [ViewComponent, CreateModalComponent, DetailsComponent],
  providers: [CustomerService],
  exports: [RouterModule],
  entryComponents: [
    CreateModalComponent
  ]
})
export class CustomerModule { }
