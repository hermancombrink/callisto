import { Component, OnInit, ViewChild } from '@angular/core';
import { AuthService } from '../../core/auth.service';
import { Router } from '@angular/router';
import { AlertService } from '../../core/alert.service';
import { TeamService } from '../../team/team.service';
import { LookupService } from '../../core/lookup.service';
import { LookupViewModel } from '../../core/models/lookupViewModel';
import { RequestStatus } from '../../core/models/requestStatus';
import { DxFormComponent } from 'devextreme-angular';
import { Location } from '@angular/common';
import { ProfileViewModel } from '../models/profileViewModel';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  @ViewChild('dxForm') dxForm: DxFormComponent;

  model: ProfileViewModel = new ProfileViewModel;
  roles: LookupViewModel[];
  manager: LookupViewModel[];
  completed = false;

  constructor(
    private router: Router,
    private alertService: AlertService,
    private authService: AuthService,
    private lookupService: LookupService,
    public _location: Location
  ) { }

  ngOnInit() {
    this.authService.currentUser.subscribe(c => {
      this.model.FirstName = c.FirstName;
      this.model.LastName = c.LastName;
    });

    this.lookupService.GetLookupData("roles").subscribe(c => {
      this.roles = c.Result;
    });

    this.lookupService.GetLookupData("manager").subscribe(c => {
      this.manager = c.Result;
    });
  }

  onSubmit() {
    let isvalid = this.dxForm.instance.validate();
    if (!isvalid.isValid) {
      return;
    }

    this.model.ProfileCompleted = true;

    this.authService.UpdateProfile(this.model).subscribe(c => {
      if (c.Status === RequestStatus.Success) {
        this.authService.GetUser().subscribe(x => {
          this.completed = true;
          this.router.navigate(['/']);
        });
        this.alertService.showSuccessMessage('Account updated!');
      } else {
        this.alertService.showWarningMessage(c.FriendlyMessage);
      }
    }, e => {
      this.alertService.showErrorMessage();
    });
  }

  isComplete() {
    return this.completed;
  }

  checkComparison() {
    return true;
  }

}
