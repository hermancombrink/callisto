import { Routes } from "@angular/router";
import { ViewComponent } from "./view/view.component";
import { DetailsComponent } from "./details/details.component";

export const AssetRoutes: Routes = [
  {
    path: 'asset',
    children: [
      { path: '', component: ViewComponent, pathMatch: 'full' },
      { path: 'details/:id', component: DetailsComponent, pathMatch: 'full' }
    ]
  }
];
