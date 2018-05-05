import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CoreModule } from '../core/core.module';
import { AlertModule, TypeaheadModule, ModalModule, TooltipModule  } from 'ngx-bootstrap';
import { RouterModule } from '@angular/router';
import { AssetRoutes } from './routes';
import { ViewComponent } from './view/view.component';
import { AssetService } from './asset.service';
import { CreateModalComponent } from './create-modal/create-modal.component';
import { DetailsComponent } from './details/details.component';
import { TreeModule } from 'ng2-tree';
import { FileUploadModule } from 'ng2-file-upload'


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    CoreModule,
    AlertModule,
    TypeaheadModule,
    TooltipModule,
    TreeModule,
    FileUploadModule,
    ModalModule.forRoot(),
    RouterModule.forChild(AssetRoutes)
  ],
  declarations: [
    ViewComponent,
    CreateModalComponent,
    DetailsComponent
  ],
  providers: [AssetService],
  exports: [RouterModule],
  entryComponents: [
    CreateModalComponent
  ]
})
export class AssetModule { }
