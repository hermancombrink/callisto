import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { SalesComponent } from './report/sales/sales.component';
import { SalesqaComponent } from './report/salesqa/salesqa.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';

export const AppRoutes: Routes = [
        { path: '', component: HomeComponent, pathMatch: 'full' },
        { path: 'reporting/sales', component: SalesComponent },
        { path: 'reporting/salesqa', component: SalesqaComponent },
        { path: 'counter', component: CounterComponent },
        { path: 'fetch-data', component: FetchDataComponent }
];


