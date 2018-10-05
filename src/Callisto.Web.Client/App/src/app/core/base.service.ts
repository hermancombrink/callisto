import { Injectable } from '@angular/core';
import { RequestResult } from './models/requestResult';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable()
export class BaseService {

  apiUrl = environment.apiUrl;
  successResponse = new RequestResult();

  constructor(protected http: HttpClient) { }

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

  getUrl(endpoint: string): string {
    return `${this.apiUrl}${endpoint}`;
  }
}
