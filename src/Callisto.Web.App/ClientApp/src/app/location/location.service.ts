import { Injectable } from '@angular/core';
import { BaseService } from '../core/base.service';
import { CacheService } from '../core/cache.service';
import { HttpClient } from '@angular/common/http';
import { LocationViewModel } from './models/locationViewModel';
import { RequestTypedResult } from '../core/models/requestResult';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class LocationService extends BaseService {

  constructor(http: HttpClient,
    private readonly cache: CacheService) {
    super(http);
  }

  GetLocations(): Observable<RequestTypedResult<LocationViewModel[]>> {
    return this.http.get<RequestTypedResult<LocationViewModel[]>>(this.getUrl(`location`), this.httpOptions);
  }

}
