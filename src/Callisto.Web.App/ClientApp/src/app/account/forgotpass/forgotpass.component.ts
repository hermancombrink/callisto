import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { BaseComponent } from '../base.component';
import { AuthService } from '../../core/auth.service';
import { RequestStatus } from '../../core/models/requestStatus';
import { ResultErrorComponent } from '../../core/result-error/result-error.component';
import { Router } from '@angular/router';
import { AlertService, MessageSeverity } from '../../core/alert.service';

@Component({
  selector: 'app-forgotpass',
  templateUrl: './forgotpass.component.html',
  styleUrls: ['./forgotpass.component.css']
})
export class ForgotpassComponent extends BaseComponent {

  @ViewChild('error') errorPanel: ResultErrorComponent;

  constructor(private authService: AuthService,
    private router: Router,
    private alertService: AlertService) {
    super();
  }

  model = {
    Email : ''
  }

  onSubmit() {
    this.authService.Forget(this.model.Email).subscribe(c => {
      if (c.Status !== RequestStatus.Success) {
        this.alertService.showWarningMessage(`${c.FriendlyMessage}`);
      } else {
        this.alertService.showSuccessMessage(`Email instructions has been sent to ${this.model.Email}`);
        this.router.navigate(['/account/login']);
      }
    }, e => {
      this.alertService.showErrorMessage();
    });
  }

}
