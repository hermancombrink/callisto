import { Injectable } from '@angular/core';
import { BaseService } from '../core/base.service';
import { HttpClient } from '@angular/common/http';
import { CacheService } from '../core/cache.service';
import { RequestResult, RequestTypedResult } from '../core/models/requestResult';
import { AddVendorViewModel, VendorViewModel } from './models/VendorViewModels';
import { Observable } from 'rxjs/Observable';
import { NewAccountViewModel } from '../core/models/newAccountViewModel';

@Injectable()
export class VendorService extends BaseService {

  vendorKey = 'global_Vendor_tree';
  constructor(http: HttpClient,
    private readonly cache: CacheService) {
    super(http);
  }

  AddVendorMember(model: AddVendorViewModel): Observable<RequestResult> {
    this.cache.remove(this.vendorKey);
    return this.http.post<RequestResult>(this.getUrl('Vendor'), model, this.httpOptions);
  }

  GetVendorMembers(): Observable<RequestTypedResult<VendorViewModel[]>> {
    return this.http.get<RequestTypedResult<VendorViewModel[]>>(this.getUrl('Vendor'), this.httpOptions);
  }

  UpdateProfile(model: NewAccountViewModel): Observable<RequestResult> {
    return this.http.post<RequestResult>(this.getUrl('Vendor/profile'), model, this.httpOptions);
  }

  RemoveVendorMember(id: string): Observable<RequestResult> {
    return this.http.delete<RequestResult>(this.getUrl(`Vendor/${id}`), this.httpOptions);
  }
}
