import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { CreateModalComponent } from '../create-modal/create-modal.component';
import { AssetService } from '../asset.service';
import { Subject, Observer, Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { AlertService, DialogType, MessageSeverity } from '../../core/alert.service';
import { AssetTreeViewModel, AssetViewModel } from '../models/assetViewModel';
import { RequestStatus } from '../../core/models/requestStatus';
import { LocationComponent } from '../../location/location.component';
import { CacheService } from '../../core/cache.service';
import { assetConstants } from '../models/constants';
import { Observable } from 'rxjs/Observable';
import { DxTreeListComponent } from 'devextreme-angular';
import { EventEmitter } from 'events';
import { ISubscription } from 'rxjs/Subscription';

@Component({
  selector: 'app-view',
  templateUrl: './view.component.html',
  styleUrls: ['./view.component.css']
})
export class ViewComponent implements OnInit, OnDestroy {

  bsModalRef: BsModalRef;
  selectedId: string;
  assetData: any;
  modalSub: ISubscription;

  constructor(
    private modalService: BsModalService,
    private assetService: AssetService,
    private alertService: AlertService,
    private router: Router
  ) { }

  ngOnInit() {
    this.modalSub = this.modalService.onHidden.subscribe(c => this.loadAssets());
    this.loadAssets();
  }

  ngOnDestroy() {
    this.modalSub.unsubscribe();
  }

  createAsset() {
    this.bsModalRef = this.modalService.show(CreateModalComponent);
  }

  loadAssets() {
    this.assetService.GetAssetTreeAll().subscribe(c => {
      if (c.Status !== RequestStatus.Success) {
        this.alertService.showWarningMessage(c.FriendlyMessage);
      } else {
        this.assetData = c.Result;
      }
    }, e => {
      this.alertService.showErrorMessage();
    });
  }

  addAsset(id: string, name: string) {
    const initialState = {
      parentId: id,
      parentName: name
    };

    this.bsModalRef = this.modalService.show(CreateModalComponent, { initialState });
  }

  removeAsset(id: string) {
    this.alertService.showDialog('Do you want to remove this item?', 'Are you sure?', MessageSeverity.warn, x => {
      this.assetService.RemoveAsset(id).subscribe(c => {
        if (c.Status !== RequestStatus.Success) {
          this.alertService.showWarningMessage(c.FriendlyMessage);
        } else {
          this.loadAssets();
          this.alertService.showSuccessMessage('Asset removed');
        }
      }, e => {
        this.alertService.showErrorMessage();
      });
    }, true);
  }

  handleSelected(e) {
    this.selectedId = e.data.Id;
    setTimeout(() => {
      e.component.updateDimensions();
    }, 30);
  }

  handleView(e) {
    this.router.navigate(['/asset/details', e.data.Id]);
    localStorage.setItem('treeListStorage', JSON.stringify(e.component.state()));
    console.log(e);
  }

  handleRemove(e) {
    if (e.data.Children) {
      this.alertService.showWarningMessage('You can only remove leaf level assets');
    } else {
      this.removeAsset(e.data.Id);
    }
  }

  handleAdd(e) {
    this.addAsset(e.data.Id, e.data.Name);
  }

  handleMoved(e) {
    this.assetService.UpdateParent(e.node.node.id, e.node.parent.node.id || '').subscribe(c => {
      if (c.Status === RequestStatus.Success) {
        this.alertService.showSuccessMessage('Asset updated');
      } else {
        this.alertService.showWarningMessage(c.FriendlyMessage);
      }
    }, err => this.alertService.showErrorMessage());
  }
}
