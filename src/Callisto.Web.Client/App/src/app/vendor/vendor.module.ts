import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ViewComponent } from './view/view.component';
import { CreateModalComponent } from './create-modal/create-modal.component';
import { FormsModule } from '@angular/forms';
import { CoreModule } from '../core/core.module';
import { AlertModule, TooltipModule, ModalModule } from 'ngx-bootstrap';
import { VendorRoutes } from './routes';
import { RouterModule } from '@angular/router';
import { TreeModule } from 'ng2-tree';
import { FileUploadModule } from 'ng2-file-upload';
import { VendorService } from './Vendor.service';
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
    RouterModule.forChild(VendorRoutes),

    DxTreeListModule, DxAutocompleteModule, DxSelectBoxModule, DxNumberBoxModule,
    DxCheckBoxModule, DxValidatorModule, DxTextBoxModule, DxFormModule,
    DxTreeViewModule, DxDropDownBoxModule, DxDataGridModule, DxRadioGroupModule
  ],
  declarations: [ViewComponent, CreateModalComponent, DetailsComponent],
  providers: [VendorService],
  exports: [RouterModule],
  entryComponents: [
    CreateModalComponent
  ]
})
export class VendorModule { }
