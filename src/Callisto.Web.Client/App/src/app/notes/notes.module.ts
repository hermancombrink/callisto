import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import {
  DxTreeListModule, DxAutocompleteModule, DxTextBoxModule, DxValidatorModule,
  DxCheckBoxModule, DxSelectBoxModule, DxNumberBoxModule, DxFormModule, DxLinearGaugeModule, DxCircularGaugeModule,
  DxTreeViewModule, DxDropDownBoxModule, DxDataGridModule, DxFileUploaderModule, DxDateBoxModule
} from 'devextreme-angular';
import { ListNotesComponent } from './list-notes/list-notes.component';
import { ListTagsComponent } from './list-tags/list-tags.component';

@NgModule({
  imports: [
    CommonModule,

    DxTreeListModule, DxAutocompleteModule, DxSelectBoxModule, DxNumberBoxModule,
    DxCheckBoxModule, DxValidatorModule, DxTextBoxModule, DxFormModule, DxLinearGaugeModule, DxCircularGaugeModule,
    DxTreeViewModule, DxDropDownBoxModule, DxDataGridModule, DxFileUploaderModule, DxDateBoxModule
  ],
  exports: [
    ListNotesComponent,
    ListTagsComponent
  ],
  declarations: [ListNotesComponent, ListTagsComponent]
})
export class NotesModule { }
