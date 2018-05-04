import { Injectable } from '@angular/core';
import { BaseService } from '../core/base.service';
import { HttpClient } from '@angular/common/http';
import { AssetAddViewModel } from './models/AssetAddViewModel';
import { RequestResult, RequestTypedResult } from '../core/models/requestResult';
import { Observable } from 'rxjs/Observable';
import { AssetViewModel } from './models/assetViewModel';
import { TreeModel } from 'ng2-tree';

@Injectable()
export class AssetService extends BaseService {

  constructor(http: HttpClient) {
    super(http);
  }

  AddAsset(model: AssetAddViewModel): Observable<RequestResult> {
    return this.http.post<RequestResult>(this.getUrl('asset/create'), model, this.httpOptions);
  }

  GetAsset(id: string): Observable<RequestTypedResult<AssetViewModel>> {
    return this.http.get<RequestTypedResult<AssetViewModel>>(this.getUrl(`asset/${id}`), this.httpOptions);
  }

  SaveAsset(model: AssetViewModel): Observable<RequestTypedResult<AssetViewModel>> {
    return this.http.put<RequestTypedResult<AssetViewModel>>(this.getUrl(`asset`), model, this.httpOptions);
  }

  GetAssetTree(): TreeModel[] {
    return [
      {
        value: 'Administration Building',
        children: [
          {
            value: '1st Floor Admin Building', children: [
              { value: 'Office No 1 Admin Building 1st Floor' },
              { value: 'Office No 2 Admin Building 1st Floor' },
              { value: 'Office No 3 Admin Building 1st Floor' }
            ]
          },
          {
            value: '2nd Floor Admin Building', children: [
              { value: 'Room 201 2nd Floor Admin Building' },
              { value: 'Room 202 2nd Floor Admin Building' },
              { value: 'Room 203 2nd Floor Admin Building' },
              { value: 'Computer Room 2nd Floor Admin Building' },
              { value: 'Plant Room 2nd Floor Admin Building' }
            ]
          }
        ]
      },
      {
        value: 'Administration Building 2',
        children: [
          {
            value: 'Building & Engineering Services', children: [
              {
                value: 'Emergency Backup Power Supply', children: [
                  { value: 'EMPS Emergency Generator' },
                  { value: 'Main Power Supply Board For Site Emergency Backup' }
                ]
              },
              {
                value: 'Boiler Services', children: [
                  {
                    value: 'Boilers', children: [
                      {
                        value: 'Factory 1 Boiler', children: [
                          { value: 'Burner No 1' },
                          { value: 'Induced Draft Fan No 32' },
                          { value: 'Boiler 3 Outlet Pump' },
                          { value: 'Standby Outlet Pump' }
                        ]
                      },
                      { value: 'Factory 2 Boiler' }
                    ]
                  },
                  {
                    value: 'Infeed Water', children: [
                      {
                        value: 'Feed Water Tank', children: [
                          { value: 'Standby Inlet Feed Pump To Boilers' },
                          { value: 'Inlet Feed Pump To Boilers' }
                        ]
                      }
                    ]
                  }
                ]
              },
              {
                value: 'Compressed Air Services', children: [
                  {
                    value: 'Compressors', children: [
                      { value: 'Air Compressor Site Services' },
                      { value: '	Air Compressor 2 Production Line 1' },
                      { value: 'Air Compressor 3 Production Line 2' }
                    ]
                  },
                  {
                    value: 'Air Dryers, Separation & Filtration', children: [
                      {
                        value: 'Refrigerant Dryer No 1', children: [
                          { value: 'Air Receiver No 1' }
                        ]
                      }
                    ]
                  },
                  { value: 'Receivers' }
                ]
              },
              { value: 'Fire Services' },
              { value: 'Heating Ventilation & Air Conditioning' },
              { value: 'Electrical Services' },
              { value: 'Maintenance & Engineering Workshop' }
            ]
          },
          {
            value: 'Production', children: [
              { value: 'Process Factory No 1' },
              { value: 'Process Factory No 2' },
            ]
          },
          {
            value: 'Warehouse', children: [
              {
                value: 'Warehouse Forklifts', children: [
                  { value: 'Forklift 01 Warehouse' },
                  { value: 'Forklift 02 Warehouse' }
                ]
              }
            ]
          },
          {
            value: 'Waste Treatment Plant 1', children: [
              {
                value: 'Waste Separation Tank', children: [
                  { value: 'Waste Separation Tank Feed Pump' },
                  { value: 'Waste Separation Tank Solids Pump' }
                ]
              },
              { value: 'WTP Effluent Tank' },
              { value: 'WTP Solids Tank' }
            ]
          }
        ]
      }
    ];
  }
}
