import 'rxjs/add/operator/do';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';


@Injectable()
export class EchoInterceptor implements HttpInterceptor {
  constructor(private router: Router) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    if ((request.method === "POST" || request.method === "PUT") && !environment.production) {
        console.info(".... start of request ....");
        console.info(request.url);
        console.log(request.body);
        console.info(".... end of request ....");
    }

    return next.handle(request);
  }
}
