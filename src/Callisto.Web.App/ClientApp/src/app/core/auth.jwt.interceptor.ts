import 'rxjs/add/operator/do';
import { HttpErrorResponse, HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private router: Router, private authService: AuthService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    return next.handle(request).do((event: HttpEvent<any>) => {
      if (event instanceof HttpResponse) {
        // do stuff with response if you want
      }
    }, (err: any) => {
      if (err instanceof HttpErrorResponse) {
        switch (err.status) {
          case 401: {
            this.authService.ClearToken();
            this.router.navigate(['/account/login']);
            break;
          }
          case 500: {
            this.router.navigate(['/error/500']);
            break;
          }
          case 404: {
            this.router.navigate(['/error/404']);
            break;
          }
          default:
            {
              break;
            }
        }
      }
    });
  }
}
