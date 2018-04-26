import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { BaseComponent } from '../base.component';
import { RegisterViewModel } from '../models/registerViewModel';
import { AuthService } from '../../core/auth.service';
import { RequestStatus } from '../../core/models/requestStatus';
import { ResultErrorComponent } from '../../core/result-error/result-error.component';

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

  model = new RegisterViewModel();

  ngOnInit() {
    super.ngOnInit();
    this.model.FirstName = "Test";
    this.model.LastName = "Test";
    this.model.CompanyName = "Test";
    this.model.Password = "123123";
    this.model.ConfirmPassword = "123123";
    this.model.Email = "test@test.com";
  }

  onSubmit() {
    this.authService.Register(this.model).subscribe(c => {
      if (c.status != RequestStatus.Success) {
        this.errorPanel.error = c.friendlyMessage;
        console.error(c.friendlyMessage);
      }
      else {
        this.errorPanel.error = '';
      }
      console.info(c);
    }, e => {
      console.error(e);
    });
  }
}
