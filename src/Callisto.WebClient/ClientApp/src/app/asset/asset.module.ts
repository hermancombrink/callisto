import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { AssetComponent } from './asset.component';
import { RouterModule } from '@angular/router';
import { AssetRoutes } from './routes';
import { DetailsComponent } from './components/details/details.component';
import { NotesComponent } from './components/notes/notes.component';
import { TreeviewComponent } from './components/treeview/treeview.component';
import { SummaryComponent } from './components/summary/summary.component';
import { ExtendedComponent } from './components/extended/extended.component';
import { AssetListComponent } from './asset-list/asset-list.component';
import { BulkRegComponent } from './bulk-reg/bulk-reg.component';
import { AssetListItemComponent } from './components/asset-list-item/asset-list-item.component';
import { TreeModule } from 'ng2-tree';
import { AssetService } from './services/asset.service';
import { AssetDetailsComponent } from './asset-details/asset-details.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    TreeModule ,
    RouterModule.forChild(AssetRoutes)
  ],
  declarations: [
    DetailsComponent,
    AssetComponent,
    NotesComponent,
    TreeviewComponent,
    SummaryComponent,
    ExtendedComponent,
    AssetListComponent,
    BulkRegComponent,
    AssetListItemComponent,
    AssetDetailsComponent],
    providers: [AssetService],
  exports: [RouterModule]
})
export class AssetModule { }
