import { Component, OnInit, OnDestroy } from '@angular/core';
import { BaseComponent } from '../base.component';
import { LoginViewModel } from '../models/loginViewModel';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent extends BaseComponent {

  constructor() {
    super();
  }

  model = new LoginViewModel();

  onSubmit() {
    console.info("Log in clicked");
  }
}
