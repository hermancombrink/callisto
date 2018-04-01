import { Routes } from '@angular/router';
import { AssetComponent } from './asset.component';
import { AssetListComponent } from './asset-list/asset-list.component';

export const AssetRoutes: Routes = [
    {
        path: 'asset',
        children: [
            { path: 'new', component: AssetComponent },
            { path: '', component: AssetListComponent, pathMatch: 'full' }
        ]
    }
];


