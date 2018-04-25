import { Component, OnInit, OnDestroy } from '@angular/core';
import { BaseComponent } from '../base.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent extends BaseComponent {

  constructor() {
    super();
  }

  model = {
    username: '',
    password: ''
  }

  onSubmit() {
    console.info("Log in clicked");
  }
}
