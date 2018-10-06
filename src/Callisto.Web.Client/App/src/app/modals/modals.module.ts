import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CreateWorkorderComponent } from './create-workorder/create-workorder.component';
import { ModalModule, AlertModule } from 'ngx-bootstrap';
import {
  DxTreeListModule, DxAutocompleteModule, DxTextBoxModule, DxValidatorModule, DxTextAreaModule,
  DxCheckBoxModule, DxSelectBoxModule, DxNumberBoxModule, DxFormModule, DxLinearGaugeModule, DxCircularGaugeModule,
  DxTreeViewModule, DxDropDownBoxModule, DxDataGridModule, DxFileUploaderModule, DxDateBoxModule
} from 'devextreme-angular';

@NgModule({
  imports: [
    CommonModule,
    ModalModule,
    AlertModule,
    FormsModule,

    DxTreeListModule, DxAutocompleteModule, DxSelectBoxModule, DxNumberBoxModule, DxTextAreaModule,
    DxCheckBoxModule, DxValidatorModule, DxTextBoxModule, DxFormModule, DxLinearGaugeModule, DxCircularGaugeModule,
    DxTreeViewModule, DxDropDownBoxModule, DxDataGridModule, DxFileUploaderModule, DxDateBoxModule
  ],
  exports : [
    CreateWorkorderComponent
  ],
  declarations: [CreateWorkorderComponent],
  entryComponents: [
    CreateWorkorderComponent
  ]
})
export class ModalsModule { }
