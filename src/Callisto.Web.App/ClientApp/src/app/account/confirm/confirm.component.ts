import { Component, OnInit, ViewChild } from '@angular/core';
import { ResultErrorComponent } from '../../core/result-error/result-error.component';
import { AlertService, MessageSeverity } from '../../core/alert.service';
import { Router, ActivatedRoute } from '@angular/router';
import { DxFormComponent } from 'devextreme-angular';
import { ConfirmViewModel } from '../models/confirmViewModel';
import { AuthService } from '../../core/auth.service';
import { RequestStatus } from '../../core/models/requestStatus';

@Component({
  selector: 'app-confirm',
  templateUrl: './confirm.component.html',
  styleUrls: ['./confirm.component.css']
})
export class ConfirmComponent implements OnInit {

  @ViewChild('error') errorPanel: ResultErrorComponent;
  @ViewChild('dxForm') dxForm: DxFormComponent;

  constructor(private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute,
    private alertService: AlertService) {

    this.route.params.subscribe(params =>
      this.model.Token = params['token']
    );
  }

  model = new ConfirmViewModel();

  ngOnInit() {
  }

  onSubmit() {
    let isvalid = this.dxForm.instance.validate();
    if (!isvalid.isValid) {
      return;
    }

    this.authService.ConfirmAccount(this.model).subscribe(c => {
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
