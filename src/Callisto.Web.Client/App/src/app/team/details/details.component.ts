import { Component, OnInit, ViewChild } from '@angular/core';
import { DxFormComponent } from 'devextreme-angular';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertService } from '../../core/alert.service';
import { AuthService } from '../../core/auth.service';
import { Location } from '@angular/common';
import { NewAccountViewModel } from '../../core/models/newAccountViewModel';
import { RequestStatus } from '../../core/models/requestStatus';
import { TeamService } from '../team.service';
import { LookupService } from '../../core/lookup.service';
import { LookupViewModel } from '../../core/models/lookupViewModel';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

  @ViewChild('dxForm') dxForm: DxFormComponent;

  model: NewAccountViewModel = new NewAccountViewModel;
  roles: LookupViewModel[];
  manager: LookupViewModel[];
  completed = false;

  constructor(
    private router: Router,
    private alertService: AlertService,
    private authService: AuthService,
    private teamService: TeamService,
    private lookupService: LookupService,
    public _location: Location
  ) {
  }

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

    this.teamService.UpdateProfile(this.model).subscribe(c => {
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
