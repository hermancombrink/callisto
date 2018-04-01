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
import { SparklineModule } from '../directives/sparkline';
import { FlotModule } from '../directives/flotChart';
import { ChartsModule } from 'ng2-charts/ng2-charts';
import { PwrbidashboardDirective } from '../directives/pwrbidashboard.directive';
import { SalesComponent } from './report/sales/sales.component';
import { SalesqaComponent } from './report/salesqa/salesqa.component';
import { PowersampleService } from '../services/powersample.service';
import { PwrbireportqaDirective } from '../directives/pwrbireportqa.directive';
import { AssetComponent } from './asset/asset.component';
import { SharedModule } from './shared/shared.module';
import { AssetModule } from './asset/asset.module';
import { AppRoutes } from './routes';
import { WorkorderModule } from './workorder/workorder.module';
import { ReportModule } from './report/report.module';


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
    SparklineModule,
    FlotModule,
    ChartsModule,
    SharedModule,
    AssetModule,
    WorkorderModule,
    ReportModule,
    ToastyModule.forRoot(),
    RouterModule.forRoot(AppRoutes)
  ],
  providers: [
    AlertService, AuthserviceService, PowersampleService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
