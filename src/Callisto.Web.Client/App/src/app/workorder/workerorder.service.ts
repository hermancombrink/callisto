import { Injectable } from '@angular/core';
import { BaseService } from '../core/base.service';
import { CacheService } from '../core/cache.service';
import { HttpClient } from '@angular/common/http';
import { NewWorkOrderViewModel } from './models/newWorkOrderViewModel';
import { RequestResult } from '../core/models/requestResult';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WorkerorderService extends BaseService {

  constructor(http: HttpClient,
    private readonly cache: CacheService) {
    super(http);
  }

  AddWorkOrder(model: NewWorkOrderViewModel): Observable<RequestResult> {
    return this.http.post<RequestResult>(this.getUrl('workorder/create'), model, this.httpOptions);
  }
}
