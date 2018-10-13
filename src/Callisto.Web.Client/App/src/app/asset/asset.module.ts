import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { AlertModule, TypeaheadModule, ModalModule, TooltipModule, TabsModule } from 'ngx-bootstrap';
import {
  DxTreeListModule, DxAutocompleteModule, DxTextBoxModule, DxValidatorModule,
  DxCheckBoxModule, DxSelectBoxModule, DxNumberBoxModule, DxFormModule, DxLinearGaugeModule, DxCircularGaugeModule,
  DxTreeViewModule, DxDropDownBoxModule, DxDataGridModule, DxFileUploaderModule, DxDateBoxModule
} from 'devextreme-angular';

import { CoreModule } from '../core/core.module';
import { AssetRoutes } from './routes';
import { AssetService } from './asset.service';

import { TreeModule } from 'ng2-tree';
import { FileUploadModule } from 'ng2-file-upload'
import { LocationModule } from '../location/location.module';
import { ModalsModule } from '../modals/modals.module';

import { ViewComponent } from './view/view.component';
import { ViewSummaryComponent } from './view-summary/view-summary.component';
import { ListWorkOrderComponent } from './list-workorder/list-workorder.component';
import { ListMainScheduleComponent } from './list-main-schedule/list-main-schedule.component';
import { CreateModalComponent } from './create-modal/create-modal.component';
import { DetailsComponent } from './details/details.component';
import { FinanceComponent } from './finance/finance.component';
import { InspectionComponent } from './inspection/inspection.component';
import { TrackGaugesComponent } from './track-gauges/track-gauges.component';
import { DocumentsModule } from '../documents/documents.module';
import { NotesModule } from '../notes/notes.module';
import { AuditModule } from '../audit/audit.module';


@NgModule({
  imports: [
    CommonModule,
    LocationModule,
    FormsModule,
    CoreModule,
    AlertModule,
    ModalsModule,
    AuditModule,
    DocumentsModule,
    NotesModule,

    TypeaheadModule,
    TooltipModule,
    TreeModule,
    FileUploadModule,
    ModalModule.forRoot(),
    TabsModule.forRoot(),
    RouterModule.forChild(AssetRoutes),

    DxTreeListModule, DxAutocompleteModule, DxSelectBoxModule, DxNumberBoxModule,
    DxCheckBoxModule, DxValidatorModule, DxTextBoxModule, DxFormModule, DxLinearGaugeModule, DxCircularGaugeModule,
    DxTreeViewModule, DxDropDownBoxModule, DxDataGridModule, DxFileUploaderModule, DxDateBoxModule
  ],
  declarations: [
    ViewComponent,
    CreateModalComponent,
    DetailsComponent,
    ViewSummaryComponent,
    ListWorkOrderComponent,
    ListMainScheduleComponent,
    FinanceComponent,
    InspectionComponent,
    TrackGaugesComponent
  ],
  providers: [AssetService],
  exports: [RouterModule],
  entryComponents: [
    CreateModalComponent
  ]
})
export class AssetModule { }
