import { Component, OnInit, ViewChild } from '@angular/core';
import { DxFormComponent } from 'devextreme-angular';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertService } from '../../core/alert.service';
import { AuthService } from '../../core/auth.service';
import { Location } from '@angular/common';
import { NewAccountViewModel, NewCompanyViewModel } from '../../core/models/newAccountViewModel';
import { RequestStatus } from '../../core/models/requestStatus';
import { UserViewModel } from '../../core/models/userViewModel';
import { StaffService } from '../staff.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

  @ViewChild('dxForm') dxForm: DxFormComponent;

  model: NewAccountViewModel = new NewAccountViewModel;
  completed = false;

  roles: string[] = [
    'Developer',
    'CEO',
    'Marketer',
    'Other'
  ];

  companysize: string[] = [
    '1 - 500',
    '501 - 1,000',
    '1,001 - 5,000',
    '5,001+'
  ];

  showCompany = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private alertService: AlertService,
    private authService: AuthService,
    private staffService: StaffService,
    public _location: Location
  ) {
    this.model.UserRole = 'Other';
    this.model.CompanyDetails.CompanySize = '1 - 500';
  }

  ngOnInit() {
    this.authService.currentUser.subscribe(c => {
      this.showCompany = !c.CompanyProfileCompleted;

      this.model.FirstName = c.FirstName;
      this.model.LastName = c.LastName;

    });
  }

  onSubmit() {
    let isvalid = this.dxForm.instance.validate();
    if (!isvalid.isValid) {
      return;
    }

    this.staffService.UpdateProfile(this.model).subscribe(c => {
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