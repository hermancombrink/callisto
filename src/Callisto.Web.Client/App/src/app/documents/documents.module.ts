import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import {
  DxTreeListModule, DxAutocompleteModule, DxTextBoxModule, DxValidatorModule,
  DxCheckBoxModule, DxSelectBoxModule, DxNumberBoxModule, DxFormModule, DxLinearGaugeModule, DxCircularGaugeModule,
  DxTreeViewModule, DxDropDownBoxModule, DxDataGridModule, DxFileUploaderModule, DxDateBoxModule
} from 'devextreme-angular';

import { ListDocumentsComponent } from './list-documents/list-documents.component';

@NgModule({
  imports: [
    CommonModule,

    DxTreeListModule, DxAutocompleteModule, DxSelectBoxModule, DxNumberBoxModule,
    DxCheckBoxModule, DxValidatorModule, DxTextBoxModule, DxFormModule, DxLinearGaugeModule, DxCircularGaugeModule,
    DxTreeViewModule, DxDropDownBoxModule, DxDataGridModule, DxFileUploaderModule, DxDateBoxModule
  ],
  exports: [
    ListDocumentsComponent
  ],
  declarations: [ListDocumentsComponent]
})
export class DocumentsModule { }
