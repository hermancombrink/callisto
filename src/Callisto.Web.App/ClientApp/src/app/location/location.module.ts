import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgmCoreModule } from '@agm/core';
import { environment } from '../../environments/environment';
import { LocationComponent } from './location.component';

@NgModule({
  imports: [
    CommonModule,
    AgmCoreModule.forRoot({
      apiKey: environment.mapsApiKey,
      libraries: ["places"]
    }),
  ],
  declarations: [LocationComponent],
  providers: [],
  exports: [LocationComponent]
})
export class LocationModule { }
