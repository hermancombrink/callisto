import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { BaseComponent } from '../base.component';
import { AuthService } from '../../core/auth.service';
import { RequestStatus } from '../../core/models/requestStatus';
import { ResultErrorComponent } from '../../core/result-error/result-error.component';
import { RegisterViewModel } from '../models/registerViewModel';
import { DxFormComponent } from 'devextreme-angular';
import { MessageSeverity, AlertService } from '../../core/alert.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent extends BaseComponent {

  constructor(
    private authService: AuthService,
    private alertService: AlertService,
    private router: Router) {
    super();
  }

  @ViewChild('error') errorPanel: ResultErrorComponent;
  @ViewChild('dxForm') dxForm: DxFormComponent;

  model = new RegisterViewModel();

  // tslint:disable-next-line:use-life-cycle-interface
  ngOnInit() {
    super.ngOnInit();
  }

  onSubmit() {
    let isvalid = this.dxForm.instance.validate();
    if (!isvalid.isValid) {
      return;
    }

    this.authService.Register(this.model).subscribe(c => {
      if (c.Status === RequestStatus.Success) {
        this.doLogin();
      } else {
        this.errorPanel.error = c.FriendlyMessage;
      }
    }, e => {
      this.alertService.showErrorMessage();
    });
  }

  doLogin() {
    this.authService.Login({
      Email: this.model.Email,
      Password: this.model.Password,
      RememberMe: false
    }).subscribe(x => {
      if (x.Status === RequestStatus.Success) {
        this.alertService.showMessage('Welcome to Callisto', '', MessageSeverity.info);
        this.router.navigate(['/']);
      } else {
        this.alertService.showWarningMessage(x.FriendlyMessage);
      }
    }, e => {
      this.alertService.showErrorMessage();
    });
  }

  passwordComparison = () => {
    return this.dxForm.instance.option('formData').Password;
  };
}
