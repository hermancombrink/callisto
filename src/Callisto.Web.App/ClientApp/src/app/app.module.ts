import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ToastyModule } from 'ng2-toasty';
import { NgHttpLoaderModule } from 'ng-http-loader/ng-http-loader.module';


import { AppComponent } from './app.component';

import { CoreModule } from './core/core.module';
import { AuthGuard } from './core/auth.guard';

import { HomeComponent } from './home/home.component';
import { NavModule } from './nav/nav.module';
import { AccountModule } from './account/account.module';
import { AppRoutes } from './routes';



@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    NgHttpLoaderModule,
    FormsModule,
    NavModule,
    AccountModule,
    CoreModule.forRoot(),
    ToastyModule.forRoot(),
    RouterModule.forRoot(AppRoutes)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
