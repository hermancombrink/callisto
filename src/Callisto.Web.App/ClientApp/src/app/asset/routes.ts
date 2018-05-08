import { Routes } from "@angular/router";
import { ViewComponent } from "./view/view.component";
import { DetailsComponent } from "./details/details.component";
import { AuthGuard } from "../core/auth.guard";

export const AssetRoutes: Routes = [
  {
    path: 'asset',
    children: [
      { path: '', component: ViewComponent, pathMatch: 'full', canActivate: [AuthGuard] },
      { path: 'details/:id', component: DetailsComponent, pathMatch: 'full', canActivate: [AuthGuard] }
    ]
  }
];
