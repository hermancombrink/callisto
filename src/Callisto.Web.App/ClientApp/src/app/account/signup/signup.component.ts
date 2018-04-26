import { Component, OnInit, OnDestroy } from '@angular/core';
import { BaseComponent } from '../base.component';
import { RegisterViewModel } from '../models/registerViewModel';
import { AuthService } from '../../core/auth.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent extends BaseComponent {

  constructor(private authService: AuthService) {
    super();
  }

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

      console.info(c.Result);
    }, e => {
      console.error(e);
    });
  }
}
