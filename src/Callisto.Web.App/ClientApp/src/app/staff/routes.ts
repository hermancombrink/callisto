import { Routes } from '@angular/router';
import { ViewComponent } from './view/view.component';
import { AuthGuard } from '../core/auth.guard';
import { DetailsComponent } from './details/details.component';
import { AccountGuard } from '../account.guard';

export const StaffRoutes: Routes = [
  {
    path: 'team',
    children: [
      { path: '', component: ViewComponent, pathMatch: 'full', canActivate: [AuthGuard] },
      { path: 'my-details', component: DetailsComponent, pathMatch: 'full', canDeactivate: [AccountGuard], canActivate: [AuthGuard] }
    ]
  }
];
