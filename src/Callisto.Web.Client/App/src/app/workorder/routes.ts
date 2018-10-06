import { Routes } from '@angular/router';
import { ViewComponent } from './view/view.component';
import { DetailsComponent } from './details/details.component';
import { AuthGuard } from '../core/auth.guard';

export const WorkOrderRoutes: Routes = [
  {
    path: 'workorder',
    children: [
      { path: '', component: ViewComponent, pathMatch: 'full', canActivate: [AuthGuard] },
      { path: 'details/:id', component: DetailsComponent, pathMatch: 'full', canActivate: [AuthGuard] }
    ]
  }
];
