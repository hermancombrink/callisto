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
import { DxValidatorModule, DxAutocompleteModule, DxTextBoxModule, DxTreeListModule } from 'devextreme-angular';
import { AuthServiceConfig, GoogleLoginProvider } from 'angular5-social-auth';
import { environment } from '../../environments/environment';

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
      providers: [AuthService, AuthGuard, AlertService, BaseService, CacheService,
        {
          provide: HTTP_INTERCEPTORS,
          useClass: JwtInterceptor,
          multi: true
        },
        {
          provide: AuthServiceConfig,
          useFactory: () => {
            let config = new AuthServiceConfig(
              [
                // {
                //   id: FacebookLoginProvider.PROVIDER_ID,
                //   provider: new FacebookLoginProvider('Your-Facebook-app-id')
                // },
                {
                  id: GoogleLoginProvider.PROVIDER_ID,
                  provider: new GoogleLoginProvider(environment.googleOAuthKey)
                },
                // {
                //   id: LinkedinLoginProvider.PROVIDER_ID,
                //   provider: new GoogleLoginProvid('Your-Linkedin-Client-Id')
                // },
              ]
            )
            return config;
          }
        }
      ]
    };
  }

}
