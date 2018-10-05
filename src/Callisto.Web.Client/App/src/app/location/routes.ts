import { Routes } from '@angular/router';
import { ViewComponent } from './view/view.component';
import { AuthGuard } from '../core/auth.guard';

export const LocationRoutes: Routes = [
  {
    path: 'location',
    children: [
      { path: '', component: ViewComponent, pathMatch: 'full', canActivate: [AuthGuard] }
    ]
  }
];
