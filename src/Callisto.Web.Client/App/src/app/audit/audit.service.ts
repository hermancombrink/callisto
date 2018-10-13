import { Injectable } from '@angular/core';
import { BaseService } from '../core/base.service';
import { CacheService } from '../core/cache.service';
import { HttpClient } from '@angular/common/http';
import { AuditViewModel } from './model/auditViewModel';
import { RequestTypedResult } from '../core/models/requestResult';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuditService extends BaseService {

  constructor(http: HttpClient,
    private readonly cache: CacheService) {
    super(http);
  }

  GetAuditHistory(entityType: string, entityId: any): Observable<RequestTypedResult<AuditViewModel[]>> {
    return this.http.get<RequestTypedResult<AuditViewModel[]>>(this.getUrl(`audit/${entityType}/${entityId}`), this.httpOptions);
  }
}
