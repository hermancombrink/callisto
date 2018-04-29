import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Router } from '@angular/router';
import { ToastyModule } from 'ng2-toasty';
import { NgHttpLoaderModule } from 'ng-http-loader/ng-http-loader.module';
import { AlertModule } from 'ngx-bootstrap';
import { GridsterModule } from 'angular-gridster2';

import { AppComponent } from './app.component';

import { CoreModule } from './core/core.module';
import { AuthGuard } from './core/auth.guard';

import { HomeComponent } from './home/home.component';
import { NavModule } from './nav/nav.module';
import { AccountModule } from './account/account.module';
import { AppRoutes } from './routes';
import { ErrorModule } from './error/error.module';
import { AssetModule } from './asset/asset.module';



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
    GridsterModule,

    NavModule,
    AccountModule,
    AssetModule,
    ErrorModule,

    CoreModule.forRoot(),
    ToastyModule.forRoot(),
    AlertModule.forRoot(),
    RouterModule.forRoot(AppRoutes)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(private router: Router) {
    router.errorHandler = (error: any) => {
      this.router.navigate(['/error/404']);
    };
  }
}
