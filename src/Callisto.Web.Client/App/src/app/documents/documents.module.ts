import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FileUploadModule } from 'ng2-file-upload';

import {
  DxTreeListModule, DxAutocompleteModule, DxTextBoxModule, DxValidatorModule,
  DxCheckBoxModule, DxSelectBoxModule, DxNumberBoxModule, DxFormModule, DxLinearGaugeModule, DxCircularGaugeModule,
  DxTreeViewModule, DxDropDownBoxModule, DxDataGridModule, DxFileUploaderModule, DxDateBoxModule
} from 'devextreme-angular';

import { ListDocumentsComponent } from './list-documents/list-documents.component';
import { UploadPicComponent } from './upload-pic/upload-pic.component';

@NgModule({
  imports: [
    CommonModule,
    FileUploadModule,

    DxTreeListModule, DxAutocompleteModule, DxSelectBoxModule, DxNumberBoxModule,
    DxCheckBoxModule, DxValidatorModule, DxTextBoxModule, DxFormModule, DxLinearGaugeModule, DxCircularGaugeModule,
    DxTreeViewModule, DxDropDownBoxModule, DxDataGridModule, DxFileUploaderModule, DxDateBoxModule
  ],
  exports: [
    ListDocumentsComponent, UploadPicComponent
  ],
  declarations: [ListDocumentsComponent, UploadPicComponent]
})
export class DocumentsModule { }
