import { Routes } from "@angular/router";
import { LoginComponent } from "./login/login.component";
import { SignupComponent } from "./signup/signup.component";
import { ForgotpassComponent } from "./forgotpass/forgotpass.component";

export const AccountRoutes: Routes = [
  {
    path: 'account',
    children: [
      { path: 'login', component: LoginComponent, pathMatch: 'full' },
      { path: 'signup', component: SignupComponent, pathMatch: 'full' },
      { path: 'forgotpass', component: ForgotpassComponent, pathMatch: 'full' },
    ]
  }
];
