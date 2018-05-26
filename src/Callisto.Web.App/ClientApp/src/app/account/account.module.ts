import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountRoutes } from './routes';
import { RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { ForgotpassComponent } from './forgotpass/forgotpass.component';
import { FormsModule } from '@angular/forms';
import { EqualValidator } from './equalvalidator.directive';
import { AlertModule } from 'ngx-bootstrap';
import { CoreModule } from '../core/core.module';
import { LoginViewModel } from './models/loginViewModel';
import { RegisterViewModel } from './models/registerViewModel';
import { BaseComponent } from './base.component';
import { DxValidatorModule, DxTextBoxModule, DxFormModule, DxRadioGroupModule } from 'devextreme-angular';
import { DetailsComponent } from './details/details.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    CoreModule,
    AlertModule.forRoot(),
    RouterModule.forChild(AccountRoutes),

    DxValidatorModule, DxTextBoxModule, DxFormModule, DxRadioGroupModule
  ],
  declarations: [LoginComponent, SignupComponent, ForgotpassComponent, EqualValidator, BaseComponent, DetailsComponent],
  exports: [RouterModule]
})
export class AccountModule { }
