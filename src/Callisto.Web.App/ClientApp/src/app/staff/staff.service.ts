import { Injectable } from '@angular/core';
import { BaseService } from '../core/base.service';
import { HttpClient } from '@angular/common/http';
import { CacheService } from '../core/cache.service';
import { RequestResult, RequestTypedResult } from '../core/models/requestResult';
import { AddStaffViewModel, StaffViewModel } from './models/staffViewModels';
import { Observable } from 'rxjs/Observable';
import { NewAccountViewModel } from '../core/models/newAccountViewModel';

@Injectable()
export class StaffService extends BaseService {

  staffKey = 'global_staff_tree';
  constructor(http: HttpClient,
    private readonly cache: CacheService) {
    super(http);
  }

  AddStaffMember(model: AddStaffViewModel): Observable<RequestResult> {
    this.cache.remove(this.staffKey);
    return this.http.post<RequestResult>(this.getUrl('staff'), model, this.httpOptions);
  }

  GetStaffMembers(): Observable<RequestTypedResult<StaffViewModel[]>> {
    return this.http.get<RequestTypedResult<StaffViewModel[]>>(this.getUrl('staff'), this.httpOptions);
  }

  UpdateProfile(model: NewAccountViewModel): Observable<RequestResult> {
    return this.http.post<RequestResult>(this.getUrl('staff/profile'), model, this.httpOptions);
  }

  RemoveStaffMember(id: string): Observable<RequestResult> {
    return this.http.delete<RequestResult>(this.getUrl(`staff/${id}`), this.httpOptions);
  }
}
