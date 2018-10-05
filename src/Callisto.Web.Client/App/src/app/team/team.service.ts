import { Injectable } from '@angular/core';
import { BaseService } from '../core/base.service';
import { HttpClient } from '@angular/common/http';
import { CacheService } from '../core/cache.service';
import { RequestResult, RequestTypedResult } from '../core/models/requestResult';
import { AddMemberViewModel, MemberViewModel } from './models/memberViewModels';
import { Observable } from 'rxjs/Observable';
import { NewAccountViewModel } from '../core/models/newAccountViewModel';

@Injectable()
export class TeamService extends BaseService {

  teamKey = 'global_team_tree';
  constructor(http: HttpClient,
    private readonly cache: CacheService) {
    super(http);
  }

  AddTeamMember(model: AddMemberViewModel): Observable<RequestResult> {
    this.cache.remove(this.teamKey);
    return this.http.post<RequestResult>(this.getUrl('team'), model, this.httpOptions);
  }

  GetTeamMembers(): Observable<RequestTypedResult<MemberViewModel[]>> {
    return this.http.get<RequestTypedResult<MemberViewModel[]>>(this.getUrl('team'), this.httpOptions);
  }

  UpdateProfile(model: NewAccountViewModel): Observable<RequestResult> {
    return this.http.put<RequestResult>(this.getUrl('team/profile'), model, this.httpOptions);
  }

  RemoveTeamMember(id: string): Observable<RequestResult> {
    return this.http.delete<RequestResult>(this.getUrl(`team/${id}`), this.httpOptions);
  }
}
