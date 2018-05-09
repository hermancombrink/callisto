import { Routes } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { AuthGuard } from "./core/auth.guard";
import { LocationComponent } from "./location/location.component";

export const AppRoutes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full', canActivate: [AuthGuard] },
  { path: 'location', component: LocationComponent, pathMatch: 'full', canActivate: [AuthGuard] }
];
