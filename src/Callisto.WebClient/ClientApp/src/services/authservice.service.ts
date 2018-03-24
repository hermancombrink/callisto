import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class AuthserviceService {

  constructor(private http: HttpClient) { }

  Logout(): Observable<any> {
    return this.http.post("/account/logout", {});
  }
}
