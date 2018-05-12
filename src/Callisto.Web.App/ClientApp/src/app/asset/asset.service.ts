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

  OnCachClear = new Subject<boolean>();

  constructor(http: HttpClient,
    private readonly cache: CacheService) {
    super(http);
  }

  AddAsset(model: AssetAddViewModel): Observable<RequestResult> {
    return this.http.post<RequestResult>(this.getUrl('asset/create'), model, this.httpOptions)
    .pipe(
      tap(c => {
        if (c.Status === RequestStatus.Success) {
          this.OnCachClear.next(true);
        }
      })
    );
  }

  UpdateParent(id: string, parentid: string): Observable<RequestResult> {
    return this.http.put<RequestResult>(this.getUrl(`asset/parent/${id}/${parentid}`), null, this.httpOptions)
    .pipe(
      tap(c => {
        if (c.Status === RequestStatus.Success) {
          this.cache.remove(assetConstants.treeCacheKey);
          this.OnCachClear.next(true);
        }
      })
    );
  }

  GetAsset(id: string): Observable<RequestTypedResult<AssetInfoViewModel>> {
    return this.http.get<RequestTypedResult<AssetInfoViewModel>>(this.getUrl(`asset/${id}`), this.httpOptions);
  }

  GetAssetDetail(id: string): Observable<RequestTypedResult<AssetDetailViewModel>> {
    return this.http.get<RequestTypedResult<AssetDetailViewModel>>(this.getUrl(`asset/details/${id}`), this.httpOptions);
  }

  SaveAsset(model: AssetViewModel): Observable<RequestTypedResult<AssetDetailViewModel>> {
    return this.http.put<RequestTypedResult<AssetDetailViewModel>>(this.getUrl(`asset`), model, this.httpOptions)
    .pipe(
      tap(c => {
        if (c.Status === RequestStatus.Success) {
          this.cache.remove(assetConstants.treeCacheKey);
          this.OnCachClear.next(true);
        }
      })
    );
  }

  GetAssetTree(id?: string): Observable<RequestTypedResult<AssetTreeViewModel[]>> {
    id = id || '';
    return this.http.get<RequestTypedResult<AssetTreeViewModel[]>>(this.getUrl(`asset/tree/${id}`), this.httpOptions);
  }

  RemoveAsset(id: string): Observable<RequestResult> {
    return this.http.delete<RequestResult>(this.getUrl(`asset/${id}`), this.httpOptions)
    .pipe(
      tap(c => {
        if (c.Status === RequestStatus.Success) {
          this.cache.remove(assetConstants.treeCacheKey);
          this.OnCachClear.next(true);
        }
      })
    );
  }
}
