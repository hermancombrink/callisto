import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CoreModule } from '../core/core.module';
import { AlertModule, TypeaheadModule } from 'ngx-bootstrap';
import { RouterModule } from '@angular/router';
import { AssetRoutes } from './routes';
import { CreateComponent } from './create/create.component';
import { ViewComponent } from './view/view.component';
import { AssetService } from './asset.service';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    CoreModule,
    AlertModule.forRoot(),
    TypeaheadModule,
    RouterModule.forChild(AssetRoutes)
  ],
  declarations: [CreateComponent, ViewComponent],
  providers: [AssetService],
  exports: [RouterModule]
})
export class AssetModule { }
