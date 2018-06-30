import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Router } from '@angular/router';
import { NgHttpLoaderModule } from 'ng-http-loader/ng-http-loader.module';
import {
  AlertModule,
  TypeaheadModule,
  TooltipModule
} from 'ngx-bootstrap';
import { GridsterModule } from 'angular-gridster2';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { AuthGuard } from './core/auth.guard';
import { AccountGuard } from './account.guard';
import { HomeComponent } from './home/home.component';
import { NavModule } from './nav/nav.module';
import { AccountModule } from './account/account.module';
import { AppRoutes } from './routes';
import { ErrorModule } from './error/error.module';
import { AssetModule } from './asset/asset.module';
import { AlertDialogComponent } from './core/alert-dialog/alert-dialog.component';
import { LocationComponent } from './location/location.component';
import { AgmCoreModule } from '@agm/core';
import { LocationModule } from './location/location.module';
import { environment } from '../environments/environment';
import {
  SocialLoginModule,
  AuthServiceConfig,
  GoogleLoginProvider,
  FacebookLoginProvider,
  LinkedinLoginProvider
} from 'angular5-social-auth';
import { TeamModule } from './team/team.module';
import { VendorModule } from './vendor/vendor.module';
import { CustomerModule } from './customer/customer.module';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    NgHttpLoaderModule,
    FormsModule,
    SocialLoginModule,
    GridsterModule,
    TypeaheadModule.forRoot(),
    TooltipModule.forRoot(),
    NavModule,
    LocationModule,
    AccountModule,
    AssetModule,
    TeamModule,
    VendorModule,
    CustomerModule,
    ErrorModule,
    CoreModule.forRoot(),
    AlertModule.forRoot(),
    RouterModule.forRoot(AppRoutes)
  ],
  providers: [
    AccountGuard
  ],
  bootstrap: [AppComponent],
  entryComponents: [
    AlertDialogComponent
  ]
})
export class AppModule {
  constructor(private router: Router) {
    router.errorHandler = (error: any) => {
      this.router.navigate(['/error/404']);
    };
  }
}
