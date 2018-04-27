import { Routes } from "@angular/router";
import { Err404Component } from "./err404/err404.component";
import { Err500Component } from "./err500/err500.component";

export const ErrorRoutes: Routes = [
  {
    path: 'error',
    children: [
      { path: '404', component: Err404Component, pathMatch: 'full' },
      { path: '500', component: Err500Component, pathMatch: 'full' }
    ]
  }
];
