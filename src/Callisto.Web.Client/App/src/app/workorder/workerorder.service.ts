import { Injectable } from '@angular/core';
import { BaseService } from '../core/base.service';
import { CacheService } from '../core/cache.service';
import { HttpClient } from '@angular/common/http';
import { BaseWorkOrderViewModel, WorkOrderItemViewModel } from './models/WorkOrderViewModel';
import { RequestResult, RequestTypedResult } from '../core/models/requestResult';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WorkerorderService extends BaseService {

  constructor(http: HttpClient,
    private readonly cache: CacheService) {
    super(http);
  }

  AddWorkOrder(model: BaseWorkOrderViewModel): Observable<RequestResult> {
    return this.http.post<RequestResult>(this.getUrl('workorder/create'), model, this.httpOptions);
  }

  GetWorkOrdersByAsset(id: string): Observable<RequestTypedResult<WorkOrderItemViewModel[]>> {
    return this.http.get<RequestTypedResult<WorkOrderItemViewModel[]>>(this.getUrl(`workorder/asset/${id}`), this.httpOptions);
  }
}
