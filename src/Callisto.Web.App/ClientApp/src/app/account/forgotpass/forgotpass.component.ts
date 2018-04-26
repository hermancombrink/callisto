import { Component, OnInit, OnDestroy } from '@angular/core';
import { BaseComponent } from '../base.component';

@Component({
  selector: 'app-forgotpass',
  templateUrl: './forgotpass.component.html',
  styleUrls: ['./forgotpass.component.css']
})
export class ForgotpassComponent extends BaseComponent {

  constructor() {
    super();
  }

  model = {
    email : ''
  }

}
