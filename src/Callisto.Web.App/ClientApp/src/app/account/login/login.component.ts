import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { BaseComponent } from '../base.component';
import { LoginViewModel } from '../models/loginViewModel';
import { AuthService } from '../../core/auth.service';
import { ResultErrorComponent } from '../../core/result-error/result-error.component';
import { RequestStatus } from '../../core/models/requestStatus';
import { Router } from '@angular/router';
import { AlertService, MessageSeverity } from '../../core/alert.service';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent extends BaseComponent {

  @ViewChild('error') errorPanel: ResultErrorComponent;

 

  constructor(private authService: AuthService,
    private router: Router,
    private alertService: AlertService) {
    super();
  }

  model = new LoginViewModel();

  ngOnInit() {
    super.ngOnInit();
    this.authService.ClearToken();
  }

  onSubmit() {
    this.authService.Login(this.model).subscribe(c => {
      if (c.status != RequestStatus.Success) {
        this.errorPanel.error = c.friendlyMessage;
      }
      else {
        this.alertService.showMessage('Welcome to Callisto', '', MessageSeverity.info);
        this.router.navigate(['/']);
      }
    }, e => {
      this.errorPanel.error = environment.httpNotFound;
    });
  }
}
