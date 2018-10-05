import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CoreModule } from '../core/core.module';
import { AlertModule, TypeaheadModule, ModalModule, TooltipModule, TabsModule } from 'ngx-bootstrap';
import { RouterModule } from '@angular/router';
import { AssetRoutes } from './routes';
import { ViewComponent } from './view/view.component';
import { AssetService } from './asset.service';
import { CreateModalComponent } from './create-modal/create-modal.component';
import { DetailsComponent } from './details/details.component';
import { TreeModule } from 'ng2-tree';
import { FileUploadModule } from 'ng2-file-upload'
import { LocationModule } from '../location/location.module';
import { ViewSummaryComponent } from './view-summary/view-summary.component';
import {
  DxTreeListModule, DxAutocompleteModule, DxTextBoxModule, DxValidatorModule,
  DxCheckBoxModule, DxSelectBoxModule, DxNumberBoxModule, DxFormModule, DxLinearGaugeModule,
  DxTreeViewModule, DxDropDownBoxModule, DxDataGridModule, DxFileUploaderModule, DxDateBoxModule
} from 'devextreme-angular';

@NgModule({
  imports: [
    CommonModule,
    LocationModule,
    FormsModule,
    CoreModule,
    AlertModule,

    TypeaheadModule,
    TooltipModule,
    TreeModule,
    FileUploadModule,
    ModalModule.forRoot(),
    TabsModule.forRoot(),
    RouterModule.forChild(AssetRoutes),

    DxTreeListModule, DxAutocompleteModule, DxSelectBoxModule, DxNumberBoxModule,
    DxCheckBoxModule, DxValidatorModule, DxTextBoxModule, DxFormModule, DxLinearGaugeModule,
    DxTreeViewModule, DxDropDownBoxModule, DxDataGridModule, DxFileUploaderModule, DxDateBoxModule
  ],
  declarations: [
    ViewComponent,
    CreateModalComponent,
    DetailsComponent,
    ViewSummaryComponent
  ],
  providers: [AssetService],
  exports: [RouterModule],
  entryComponents: [
    CreateModalComponent
  ]
})
export class AssetModule { }
