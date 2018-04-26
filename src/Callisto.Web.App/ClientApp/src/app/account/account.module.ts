import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountRoutes } from './routes';
import { RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { ForgotpassComponent } from './forgotpass/forgotpass.component';
import { FormsModule } from '@angular/forms';
import { EqualValidator } from './equalvalidator.directive';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild(AccountRoutes)
  ],
  declarations: [LoginComponent, SignupComponent, ForgotpassComponent, EqualValidator],
  exports: [RouterModule]
})
export class AccountModule { }
