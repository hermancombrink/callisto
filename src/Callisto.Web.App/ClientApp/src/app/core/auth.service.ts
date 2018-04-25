import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment'
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs/Observable';


@Injectable()
export class AuthService {

  apiUrl = environment.apiUrl;

  constructor() { }

  IsAuthenticated() {
    return !!localStorage.getItem('auth_token');
  }
}
