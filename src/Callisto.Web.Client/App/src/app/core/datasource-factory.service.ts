import { Injectable } from '@angular/core';
import DataSource from "devextreme/data/data_source";
import { LookupService } from './lookup.service';

@Injectable({
  providedIn: 'root'
})
export class DatasourceFactoryService {

  constructor(private lookupService: LookupService) { }

  GetAutoCompleteLookup(lookupType: string): DataSource {
    const store = new DataSource({
      key: "id",
      load: (e) => {
        return this.lookupService.GetDBLookupData(lookupType, e.searchValue, e.skip, e.take, e.sort).toPromise().then(result => {
          return {
              data: result.Result
          };
        });
      },
      byKey: (id) => {
        return this.lookupService.GetDBLookupDataById(lookupType, id).toPromise().then(result => {
          return result.Result;
        });
      }
    });
    return store;
  }

  GetTagLookup(lookupType: string): DataSource {
    const store = new DataSource({
      key: "id",
      load: (e) => {
        let values = "";

        try {
         values = (e.filter as Array<any>).map(x => {
          let y = x ? (x as Array<any>) : [];
          return y.pop ? y.pop() : "";
        }).filter(el => el).join(",");
        } catch {
        }

        return this.lookupService.GetDBLookupData(lookupType, values, e.skip, e.take, e.sort).toPromise().then(result => {
          return result.Result;
        });
      },
      byKey: (id) => {
        return this.lookupService.GetDBLookupDataById(lookupType, id).toPromise().then(result => {
          return result.Result;
        });
      }
    });
    return store;
  }
}
