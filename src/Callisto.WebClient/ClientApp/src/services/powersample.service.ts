import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class PowersampleService {

  constructor(private http: HttpClient) { }

  getReportAccessData(): Observable<PowerBiResponse> {
    return this.http.get<PowerBiResponse>('https://powerbilivedemobe.azurewebsites.net/api/Reports/SampleReport');
  }

  getQAAccessData(): Observable<PowerBiResponse> {
    return this.http.get<PowerBiResponse>('https://powerbilivedemobe.azurewebsites.net/api/Datasets/SampleQna');
  }
}

export interface PowerBiResponse {
  id: string;
  embedUrl: string;
  type: string;
  minutesToExpiration: number;
  embedToken: PowerBiToken;
}

export interface PowerBiToken {
  token: string;
  tokenId: string;
  expiration: string;
}
