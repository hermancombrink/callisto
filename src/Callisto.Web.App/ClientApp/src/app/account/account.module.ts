import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountRoutes } from './routes';
import { RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(AccountRoutes)
  ],
  declarations: [LoginComponent, SignupComponent]
})
export class AccountModule { }
