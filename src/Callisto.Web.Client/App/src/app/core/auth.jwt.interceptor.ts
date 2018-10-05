import 'rxjs/add/operator/do';
import { HttpErrorResponse, HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { AlertService } from './alert.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private router: Router,
    private authService: AuthService,
    private alertService: AlertService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    return next.handle(request).do((event: HttpEvent<any>) => {
    }, (err: any) => {
      if (err instanceof HttpErrorResponse) {
        if (err.status === 401) {
          if (this.router.url.indexOf('/account/') < 0) {
            this.router.navigate(['/account/login']);
          }
        }

        if (err.status === 404) {
          this.alertService.showWarningMessage("Oops we missing something here");
        }
      }
    });
  }
}
