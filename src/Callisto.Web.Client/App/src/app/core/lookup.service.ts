import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs-compat';
import { RequestResult, RequestTypedResult } from './models/requestResult';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CacheService } from './cache.service';
import { LookupViewModel } from './models/lookupViewModel';

@Injectable()
export class LookupService {

  apiUrl = environment.apiUrl;
  successResponse = new RequestResult();
  loggedIn = new Subject<RequestResult>();
  loggedOut = new Subject<RequestResult>();

  constructor(
    private http: HttpClient,
    private readonly cache: CacheService) {
  }

  get httpOptions() {
    return {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${localStorage.getItem('auth_token')}`
      })
    };
  }

  GetJobRoles(): Observable<RequestTypedResult<LookupViewModel[]>> {
    return this.http.get<RequestTypedResult<LookupViewModel[]>>(this.getUrl('lookup/roles'), this.httpOptions);
  }

  GetJobManager(): Observable<RequestTypedResult<LookupViewModel[]>> {
    return this.http.get<RequestTypedResult<LookupViewModel[]>>(this.getUrl('lookup/manager'), this.httpOptions);
  }

  GetReadingTypes(): Observable<RequestTypedResult<LookupViewModel[]>> {
    return this.http.get<RequestTypedResult<LookupViewModel[]>>(this.getUrl('lookup/readingtype'), this.httpOptions);
  }

  GetStatusTypes(): Observable<RequestTypedResult<LookupViewModel[]>> {
    return this.http.get<RequestTypedResult<LookupViewModel[]>>(this.getUrl('lookup/statustype'), this.httpOptions);
  }

  GetCriticalTypes(): Observable<RequestTypedResult<LookupViewModel[]>> {
    return this.http.get<RequestTypedResult<LookupViewModel[]>>(this.getUrl('lookup/criticaltype'), this.httpOptions);
  }

  GetManufacturers(): Observable<RequestTypedResult<LookupViewModel[]>> {
    return this.http.get<RequestTypedResult<LookupViewModel[]>>(this.getUrl('dblookup/manufacturer'), this.httpOptions);
  }

  private getUrl(endpoint: string): string {
    return `${this.apiUrl}${endpoint}`;
  }
}
