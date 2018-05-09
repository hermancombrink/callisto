import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { BaseComponent } from '../base.component';
import { AuthService } from '../../core/auth.service';
import { RequestStatus } from '../../core/models/requestStatus';
import { ResultErrorComponent } from '../../core/result-error/result-error.component';
import { RegisterViewModel } from '../models/registerViewModel';

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

  isRregistered: boolean = false;

  ngOnInit() {
    super.ngOnInit();
  }

  onSubmit() {
    this.authService.Register(this.model).subscribe(c => {
      if (c.Status != RequestStatus.Success) {
        this.errorPanel.error = c.FriendlyMessage;
      }
      else {
        this.isRregistered = true;
        this.errorPanel.error = '';
      }
      console.info(c);
    }, e => {
      console.error(e);
    });
  }
}
