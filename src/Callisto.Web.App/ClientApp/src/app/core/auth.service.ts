import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment'
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs/Rx';
import { RegisterViewModel } from '../account/models/registerViewModel';
import { RequestResult, RequestTypedResult } from './models/requestResult';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { SpinnerVisibilityService } from 'ng-http-loader/services/spinner-visibility.service';
import { RequestStatus } from './models/requestStatus';
import { tap, catchError } from 'rxjs/operators';
import { LoginViewModel } from '../account/models/loginViewModel';
import { UserViewModel } from './models/userViewModel';
import { Router } from '@angular/router';

@Injectable()
export class AuthService {

  apiUrl = environment.apiUrl;
  successResponse = new RequestResult();

  constructor(private http: HttpClient) { }

  IsAuthenticated() {
    return !!localStorage.getItem('auth_token');
  }

  ClearToken() {
    localStorage.removeItem('auth_token');
  }

  get httpOptions() {
    return {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${localStorage.getItem('auth_token')}`
      })
    };
  }

  get authToken() {
    return `Bearer ${localStorage.getItem('auth_token')}`;
  }

  Register(model: RegisterViewModel): Observable<RequestResult> {
    return this.http.post<RequestResult>(this.getUrl('auth/signup'), model, this.httpOptions);
  }

  Login(model: LoginViewModel): Observable<RequestResult> {
    return this.http.post<RequestResult>(this.getUrl('auth/login'), model, this.httpOptions).pipe(
      tap(c => {
        if (c.Status === RequestStatus.Success) {
          localStorage.setItem('auth_token', c.Result);
        }
      }));
  }

  Forget(email: string): Observable<RequestResult> {
    return this.http.get<RequestResult>(this.getUrl(`auth/forgot?email=${email}`), this.httpOptions);
  }

  SignOut(): Observable<RequestResult> {
    return this.http.get<RequestResult>(this.getUrl('auth/signout'), this.httpOptions).pipe(
      tap(c => {
        if (c.Status === RequestStatus.Success) {
          this.ClearToken();
        }
      })
    )
  }

  GetUser(): Observable<RequestTypedResult<UserViewModel>> {
    return this.http.get<RequestTypedResult<UserViewModel>>(this.getUrl('auth/user'), this.httpOptions)
  }

  private getUrl(endpoint: string): string {
    return `${this.apiUrl}${endpoint}`;
  }
}
