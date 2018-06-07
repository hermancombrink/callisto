import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { ForgotpassComponent } from './forgotpass/forgotpass.component';
import { AccountGuard } from '../account.guard';
import { AuthGuard } from '../core/auth.guard';
import { ConfirmComponent } from './confirm/confirm.component';
import { ResetComponent } from './reset/reset.component';

export const AccountRoutes: Routes = [
  {
    path: 'account',
    children: [
      { path: 'login', component: LoginComponent, pathMatch: 'full' },
      { path: 'signup', component: SignupComponent, pathMatch: 'full' },
      { path: 'forgotpass', component: ForgotpassComponent, pathMatch: 'full' },
      { path: 'confirm/:token', component: ConfirmComponent, pathMatch: 'full' },
      { path: 'reset/:token', component: ResetComponent, pathMatch: 'full' }
    ]
  }
];
