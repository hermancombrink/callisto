import { Injectable } from '@angular/core';
import { BaseService } from '../core/base.service';
import { HttpClient } from '@angular/common/http';
import { AssetAddViewModel } from './models/AssetAddViewModel';
import { RequestResult, RequestTypedResult } from '../core/models/requestResult';
import { Observable } from 'rxjs/Observable';
import { AssetViewModel, AssetInfoViewModel, AssetTreeViewModel, AssetDetailViewModel } from './models/assetViewModel';
import { TreeModel } from 'ng2-tree';
import { tap } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { RequestStatus } from '../core/models/requestStatus';
import { CacheService } from '../core/cache.service';
import { assetConstants } from './models/constants';

@Injectable()
export class AssetService extends BaseService {

  assetKey = 'global_asset_tree';
  constructor(http: HttpClient,
    private readonly cache: CacheService) {
    super(http);
  }

  AddAsset(model: AssetAddViewModel): Observable<RequestResult> {
    this.cache.remove(this.assetKey);
    return this.http.post<RequestResult>(this.getUrl('asset/create'), model, this.httpOptions);
  }

  UpdateParent(id: string, parentid: string): Observable<RequestResult> {
    this.cache.remove(this.assetKey);
    return this.http.put<RequestResult>(this.getUrl(`asset/parent/${id}/${parentid}`), null, this.httpOptions);
  }

  GetAsset(id: string): Observable<RequestTypedResult<AssetInfoViewModel>> {
    return this.http.get<RequestTypedResult<AssetInfoViewModel>>(this.getUrl(`asset/${id}`), this.httpOptions);
  }

  GetAssetDetail(id: string): Observable<RequestTypedResult<AssetDetailViewModel>> {
    return this.http.get<RequestTypedResult<AssetDetailViewModel>>(this.getUrl(`asset/details/${id}`), this.httpOptions);
  }

  SaveAsset(model: AssetViewModel): Observable<RequestTypedResult<AssetDetailViewModel>> {
    this.cache.remove(this.assetKey);
    return this.http.put<RequestTypedResult<AssetDetailViewModel>>(this.getUrl(`asset`), model, this.httpOptions);
  }

  GetAssetTree(id?: string): Observable<RequestTypedResult<AssetTreeViewModel[]>> {
    id = id || '';
    return this.http.get<RequestTypedResult<AssetTreeViewModel[]>>(this.getUrl(`asset/tree/${id}`), this.httpOptions);
  }

  GetAssetTreeAll(): Observable<RequestTypedResult<AssetTreeViewModel[]>> {
    if (this.cache.has(this.assetKey)) {
      return this.cache.get(this.assetKey);
    }

    return this.http.get<RequestTypedResult<AssetTreeViewModel[]>>(this.getUrl(`asset/tree/all`), this.httpOptions).pipe(
      tap(c => {
        if (c.Status === RequestStatus.Success)  {
          this.cache.set(this.assetKey, c);
        }
      })
    );
  }

  GetAssetTreeParents(id: string): Observable<RequestTypedResult<AssetTreeViewModel[]>> {
    return this.http.get<RequestTypedResult<AssetTreeViewModel[]>>(this.getUrl(`asset/tree/parents/${id}`), this.httpOptions);
  }

  RemoveAsset(id: string): Observable<RequestResult> {
    this.cache.remove(this.assetKey);
    return this.http.delete<RequestResult>(this.getUrl(`asset/${id}`), this.httpOptions);
  }
}
