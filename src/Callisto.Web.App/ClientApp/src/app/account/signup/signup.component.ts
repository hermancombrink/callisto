import { Component, OnInit, OnDestroy } from '@angular/core';
import { BaseComponent } from '../base.component';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent extends BaseComponent {

  constructor() {
    super();
  }
}
