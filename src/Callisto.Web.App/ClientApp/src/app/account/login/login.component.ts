import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { BaseComponent } from '../base.component';
import { LoginViewModel } from '../models/loginViewModel';
import { AuthService } from '../../core/auth.service';
import { ResultErrorComponent } from '../../core/result-error/result-error.component';
import { RequestStatus } from '../../core/models/requestStatus';
import { Router } from '@angular/router';
import { AlertService, MessageSeverity } from '../../core/alert.service';
import { environment } from '../../../environments/environment';
import { DxFormComponent } from 'devextreme-angular';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent extends BaseComponent {

  @ViewChild('error') errorPanel: ResultErrorComponent;
  @ViewChild('dxForm') dxForm: DxFormComponent;
  model = new LoginViewModel();

  constructor(private authService: AuthService,
    private router: Router,
    private alertService: AlertService) {
    super();
  }

  // tslint:disable-next-line:use-life-cycle-interface
  ngOnInit() {
    super.ngOnInit();
    this.authService.ClearToken();
  }

  onSubmit() {
    let isvalid = this.dxForm.instance.validate();
    if (!isvalid.isValid) {
      return;
    }

    this.authService.Login(this.model).subscribe(c => {
      if (c.Status === RequestStatus.Success) {
        this.alertService.showMessage('Welcome to Callisto', '', MessageSeverity.info);
        this.router.navigate(['/']);
      } else {
        this.alertService.showWarningMessage(c.FriendlyMessage);
      }
    }, e => {
      this.alertService.showErrorMessage();
    });
  }

  onGoogleLogin() {
    this.authService.LoginWithGoogle().then(user => {
      this.authService.LoginWithSocial(user).subscribe(c => {
        if (c.Status === RequestStatus.Success) {
          this.alertService.showMessage('Welcome to Callisto', '', MessageSeverity.info);
          this.router.navigate(['/']);
        } else {
          this.alertService.showWarningMessage(c.FriendlyMessage);
        }
      }, e => {
        this.alertService.showErrorMessage();
      });
    }).catch(err => this.alertService.showErrorMessage());
  }
}
