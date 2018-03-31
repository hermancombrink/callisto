import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';

import { AlertService } from '../services/alert.service';
import { ToastyModule } from 'ng2-toasty';
import { TopMenuComponent } from './top-menu/top-menu.component';
import { AuthserviceService } from '../services/authservice.service';
import { IboxtoolsModule } from './iboxtools/iboxtools.module';
import { PeityModule } from '../directives/peity';
import { SparklineModule } from '../directives/sparkline';
import { FlotModule } from '../directives/flotChart';
import { ChartsModule } from 'ng2-charts/ng2-charts';
import { PwrbidashboardDirective } from '../directives/pwrbidashboard.directive';
import { SalesComponent } from './report/sales/sales.component';
import { SalesqaComponent } from './report/salesqa/salesqa.component';
import { PowersampleService } from '../services/powersample.service';
import { PwrbireportqaDirective } from '../directives/pwrbireportqa.directive';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    TopMenuComponent,
    PwrbidashboardDirective,
    SalesComponent,
    SalesqaComponent,
    PwrbireportqaDirective
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    IboxtoolsModule,
    PeityModule,
    SparklineModule,
    FlotModule,
    ChartsModule,
    ToastyModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'reporting/sales', component: SalesComponent },
      { path: 'reporting/salesqa', component: SalesqaComponent },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
    ])
  ],
  providers: [
    AlertService, AuthserviceService, PowersampleService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
