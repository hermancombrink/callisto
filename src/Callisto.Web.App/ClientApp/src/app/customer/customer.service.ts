import { Injectable } from '@angular/core';
import { BaseService } from '../core/base.service';
import { HttpClient } from '@angular/common/http';
import { CacheService } from '../core/cache.service';
import { RequestResult, RequestTypedResult } from '../core/models/requestResult';
import { AddCustomerViewModel, CustomerViewModel } from './models/CustomerViewModels';
import { Observable } from 'rxjs/Observable';
import { NewAccountViewModel } from '../core/models/newAccountViewModel';

@Injectable()
export class CustomerService extends BaseService {

  CustomerKey = 'global_Customer_tree';
  constructor(http: HttpClient,
    private readonly cache: CacheService) {
    super(http);
  }

  AddCustomerMember(model: AddCustomerViewModel): Observable<RequestResult> {
    this.cache.remove(this.CustomerKey);
    return this.http.post<RequestResult>(this.getUrl('Customer'), model, this.httpOptions);
  }

  GetCustomerMembers(): Observable<RequestTypedResult<CustomerViewModel[]>> {
    return this.http.get<RequestTypedResult<CustomerViewModel[]>>(this.getUrl('Customer'), this.httpOptions);
  }

  UpdateProfile(model: NewAccountViewModel): Observable<RequestResult> {
    return this.http.post<RequestResult>(this.getUrl('Customer/profile'), model, this.httpOptions);
  }

  RemoveCustomerMember(id: string): Observable<RequestResult> {
    return this.http.delete<RequestResult>(this.getUrl(`Customer/${id}`), this.httpOptions);
  }
}
