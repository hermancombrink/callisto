import { Routes } from '@angular/router';
import { ViewComponent } from './view/view.component';
import { AuthGuard } from '../core/auth.guard';

export const StaffRoutes: Routes = [
  {
    path: 'team',
    children: [
      { path: '', component: ViewComponent, pathMatch: 'full', canActivate: [AuthGuard] }
    ]
  }
];
