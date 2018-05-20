import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { BaseComponent } from '../base.component';
import { AuthService } from '../../core/auth.service';
import { RequestStatus } from '../../core/models/requestStatus';
import { ResultErrorComponent } from '../../core/result-error/result-error.component';
import { RegisterViewModel } from '../models/registerViewModel';
import { DxFormComponent } from 'devextreme-angular';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent extends BaseComponent {

  constructor(private authService: AuthService) {
    super();
  }

  @ViewChild('error') errorPanel: ResultErrorComponent;
  @ViewChild('dxForm') dxForm: DxFormComponent;

  model = new RegisterViewModel();

  isRregistered = false;

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
      if (c.Status !== RequestStatus.Success) {
        this.errorPanel.error = c.FriendlyMessage;
      } else {
        this.isRregistered = true;
        this.errorPanel.error = '';
      }
      console.info(c);
    }, e => {
      console.error(e);
    });
  }

  passwordComparison = () => {
    return this.dxForm.instance.option('formData').Password;
};
}
