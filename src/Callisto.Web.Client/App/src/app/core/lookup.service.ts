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

  GetLookupData(lookUpType: string, filter: string = '', skip: number = 0, take: number = 10, sort: boolean = true): Observable<RequestTypedResult<LookupViewModel[]>> {
    return this.http.get<RequestTypedResult<LookupViewModel[]>>(this.getUrl(`lookup/${lookUpType}?filter=${filter}&skip=${skip}&take=${take}&sortAsc=${sort}`), this.httpOptions);
  }

  GetLookupDataById(lookUpType: string, id: number, filter: string = '', skip: number = 10, take: number = 100, sort: boolean = true): Observable<RequestTypedResult<LookupViewModel[]>> {
    return this.http.get<RequestTypedResult<LookupViewModel[]>>(this.getUrl(`lookup/${lookUpType}/${id ? id : 0}?filter=${filter}&skip=${skip}&take=${take}&sortAsc=${sort}`), this.httpOptions);
  }

  GetDBLookupData(lookUpType: string, filter: string = '', skip: number = 0, take: number = 10, sort: boolean = true): Observable<RequestTypedResult<LookupViewModel[]>> {
    return this.http.get<RequestTypedResult<LookupViewModel[]>>(this.getUrl(`dblookup/${lookUpType}?filter=${filter}&skip=${skip}&take=${take}&sortAsc=${sort}`), this.httpOptions);
  }

  GetDBLookupDataById(lookUpType: string, id: number, filter: string = '', skip: number = 0, take: number = 10, sort: boolean = true): Observable<RequestTypedResult<LookupViewModel[]>> {
    return this.http.get<RequestTypedResult<LookupViewModel[]>>(this.getUrl(`dblookup/${lookUpType}/${id ? id : 0}?filter=${filter}&skip=${skip}&take=${take}&sortAsc=${sort}`), this.httpOptions);
  }

  private getUrl(endpoint: string): string {
    return `${this.apiUrl}${endpoint}`;
  }
}
