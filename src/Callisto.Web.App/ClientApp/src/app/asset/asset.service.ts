import { Injectable } from '@angular/core';
import { BaseService } from '../core/base.service';
import { HttpClient } from '@angular/common/http';
import { AssetAddViewModel } from './models/AssetAddViewModel';
import { RequestResult, RequestTypedResult } from '../core/models/requestResult';
import { Observable } from 'rxjs/Observable';
import { AssetViewModel, AssetTreeViewModel, AssetDetailViewModel } from './models/assetViewModel';
import { TreeModel } from 'ng2-tree';

@Injectable()
export class AssetService extends BaseService {

  constructor(http: HttpClient) {
    super(http);
  }

  AddAsset(model: AssetAddViewModel): Observable<RequestResult> {
    return this.http.post<RequestResult>(this.getUrl('asset/create'), model, this.httpOptions);
  }

  UpdateParent(id: string, parentid: string): Observable<RequestResult> {
    return this.http.put<RequestResult>(this.getUrl(`asset/parent/${id}/${parentid}`), null, this.httpOptions);
  }

  GetAsset(id: string): Observable<RequestTypedResult<AssetViewModel>> {
    return this.http.get<RequestTypedResult<AssetViewModel>>(this.getUrl(`asset/${id}`), this.httpOptions);
  }

  GetAssetDetail(id: string): Observable<RequestTypedResult<AssetDetailViewModel>> {
    return this.http.get<RequestTypedResult<AssetDetailViewModel>>(this.getUrl(`asset/details/${id}`), this.httpOptions);
  }

  SaveAsset(model: AssetViewModel): Observable<RequestTypedResult<AssetDetailViewModel>> {
    return this.http.put<RequestTypedResult<AssetDetailViewModel>>(this.getUrl(`asset`), model, this.httpOptions);
  }

  GetAssetTree(id?: string): Observable<RequestTypedResult<AssetTreeViewModel[]>> {
    id = id || '';
    return this.http.get<RequestTypedResult<AssetTreeViewModel[]>>(this.getUrl(`asset/tree/${id}`), this.httpOptions);
  }
}
