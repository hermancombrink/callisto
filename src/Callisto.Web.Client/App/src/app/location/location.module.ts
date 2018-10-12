import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgmCoreModule } from '@agm/core';
import { environment } from '../../environments/environment';
import { LocationComponent } from './location.component';
import { ViewComponent } from './view/view.component';
import { RouterModule } from '@angular/router';
import { LocationRoutes } from './routes';
import { LocationService } from './location.service';
import { DxDataGridModule, DxValidatorModule, DxTextBoxModule, DxFormModule } from 'devextreme-angular';
import { ViewSummaryComponent } from './view-summary/view-summary.component';
import { FormsModule } from '@angular/forms';
import { SearchComponent } from './search/search.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    DxDataGridModule, DxValidatorModule, DxTextBoxModule, DxFormModule,
    RouterModule.forChild(LocationRoutes),
    AgmCoreModule.forRoot({
      apiKey: environment.googleApiKey,
      libraries: ['places']
    }),
  ],
  declarations: [LocationComponent, ViewComponent, ViewSummaryComponent, SearchComponent],
  providers: [LocationService],
  exports: [LocationComponent, SearchComponent]
})
export class LocationModule { }
