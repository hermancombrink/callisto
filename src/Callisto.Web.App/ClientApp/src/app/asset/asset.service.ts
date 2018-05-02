import { Injectable } from '@angular/core';
import { BaseService } from '../core/base.service';
import { HttpClient } from '@angular/common/http';
import { AssetAddViewModel } from './models/AssetAddViewModel';
import { RequestResult } from '../core/models/requestResult';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class AssetService extends BaseService {

  constructor(http: HttpClient) {
    super(http);
  }

  AddAsset(model: AssetAddViewModel): Observable<RequestResult> {
    return this.http.post<RequestResult>(this.getUrl('asset/create'), model, this.httpOptions);
  }

}
