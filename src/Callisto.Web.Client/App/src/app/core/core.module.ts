import { NgModule, ModuleWithProviders, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from './auth.service';
import { AuthGuard } from './auth.guard';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ResultErrorComponent } from './result-error/result-error.component';
import { AlertModule } from 'ngx-bootstrap';
import { AlertService } from './alert.service';
import { JwtInterceptor } from './auth.jwt.interceptor';
import { BaseService } from './base.service';
import { AlertDialogComponent } from './alert-dialog/alert-dialog.component';
import { CacheService } from './cache.service';
import { EchoInterceptor } from './echo.interceptor';
import { LookupService } from './lookup.service';

@NgModule({
  imports: [
    CommonModule,
    HttpClientModule,
    AlertModule.forRoot()
  ],
  declarations: [ResultErrorComponent, AlertDialogComponent],
  providers: [],
  exports: [ResultErrorComponent, AlertDialogComponent]
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    if (parentModule) {
      throw new Error(
        'CoreModule is already loaded. Import it in the AppModule only');
    }
  }

  static forRoot(): ModuleWithProviders {
    return {
      ngModule: CoreModule,
      providers: [AuthService, AuthGuard, AlertService, BaseService, CacheService, LookupService,
        {
          provide: HTTP_INTERCEPTORS,
          useClass: JwtInterceptor,
          multi: true
        },
        {
          provide: HTTP_INTERCEPTORS,
          useClass: EchoInterceptor,
          multi: true
        }
        /*,
        {
          provide: AuthServiceConfig,
          useFactory: getAuthServiceConfigs
        }*/
      ]
    };
  }
}