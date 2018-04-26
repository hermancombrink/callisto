import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment'
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs/Rx';
import { RegisterViewModel } from '../account/models/registerViewModel';
import { RequestResult } from './models/requestResult';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { SpinnerVisibilityService } from 'ng-http-loader/services/spinner-visibility.service';
import { RequestStatus } from './models/requestStatus';
import { tap, catchError } from 'rxjs/operators';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    //'Authorization': `Bearer ${localStorage.getItem('auth_token')}`
  })
};

@Injectable()
export class AuthService {

  apiUrl = environment.apiUrl;

 

  constructor(private http: HttpClient, private spinner: SpinnerVisibilityService) { }

  IsAuthenticated() {
    return !!localStorage.getItem('auth_token');
  }

  Register(model: RegisterViewModel): Observable<RequestResult> {
    //let result = new RequestResult();
    //result.Status = RequestStatus.Success;
    //result.Result = '';
    //result.FriendlyMessage = '';
    //this.spinner.show();
    //return Observable.of(result).delay(1000).pipe(
    //  tap(_ => this.spinner.hide())
    //);

    return this.http.post<RequestResult>(this.getUrl('auth/signup'), model, httpOptions).pipe(
      //tap(c => {
      //  if (c.status == RequestStatus.Success) {
      //    localStorage.setItem('auth_token', c.result);
      //  }
      //})
    );
  }

  private getUrl(endpoint: string): string {
    return `${this.apiUrl}${endpoint}`;
  }
}
