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
    email : ''
  }

  onSubmit() {
    this.authService.Forget(this.model.email).subscribe(c => {
      if (c.status != RequestStatus.Success) {
        this.errorPanel.error = c.friendlyMessage;
      }
      else {
        this.alertService.showMessage('Password Reset', 'Email instructions has been sent', MessageSeverity.info);
        this.router.navigate(['/account/login']);
      }
      console.info(c);
    }, e => {
      console.error(e);
    });
  }

}
