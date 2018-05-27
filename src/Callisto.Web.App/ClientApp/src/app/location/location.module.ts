import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgmCoreModule } from '@agm/core';
import { environment } from '../../environments/environment';
import { LocationComponent } from './location.component';
import { ViewComponent } from './view/view.component';
import { RouterModule } from '@angular/router';
import { LocationRoutes } from './routes';
import { LocationService } from './location.service';
import { DxDataGridModule } from 'devextreme-angular';

@NgModule({
  imports: [
    CommonModule,
    DxDataGridModule,
    RouterModule.forChild(LocationRoutes),
    AgmCoreModule.forRoot({
      apiKey: environment.googleApiKey,
      libraries: ['places']
    }),
  ],
  declarations: [LocationComponent, ViewComponent],
  providers: [LocationService],
  exports: [LocationComponent]
})
export class LocationModule { }
