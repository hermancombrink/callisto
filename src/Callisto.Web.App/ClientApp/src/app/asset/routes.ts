import { Routes } from "@angular/router";
import { ViewComponent } from "./view/view.component";
import { CreateComponent } from "./create/create.component";

export const AssetRoutes: Routes = [
  {
    path: 'asset',
    children: [
      { path: '', component: ViewComponent, pathMatch: 'full' },
      { path: 'new', component: CreateComponent, pathMatch: 'full' }
    ]
  }
];
