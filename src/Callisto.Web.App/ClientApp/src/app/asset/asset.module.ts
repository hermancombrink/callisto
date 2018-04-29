import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CoreModule } from '../core/core.module';
import { AlertModule } from 'ngx-bootstrap';
import { RouterModule } from '@angular/router';
import { AssetRoutes } from './routes';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    CoreModule,
    AlertModule.forRoot(),
    RouterModule.forChild(AssetRoutes)
  ],
  declarations: [],
  exports: [RouterModule]
})
export class AssetModule { }
