import { Component, OnInit, ViewChild } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { TreeModule, TreeModel, NodeMenuItemAction, MenuItemSelectedEvent, Tree } from 'ng2-tree';
import { CreateModalComponent } from '../create-modal/create-modal.component';
import { AssetService } from '../asset.service';
import { TreeStatus, Ng2TreeSettings } from 'ng2-tree/src/tree.types';
import { Subject } from 'rxjs';
import { Router } from '@angular/router';
import { AlertService, DialogType, MessageSeverity } from '../../core/alert.service';
import { AssetTreeViewModel, AssetViewModel } from '../models/assetViewModel';
import { RequestStatus } from '../../core/models/requestStatus';
import { LocationComponent } from '../../location/location.component';

@Component({
  selector: 'app-view',
  templateUrl: './view.component.html',
  styleUrls: ['./view.component.css']
})
export class ViewComponent implements OnInit {
  bsModalRef: BsModalRef;
  assets: TreeModel[];
  tree: TreeModel;
  selectedId: string;

  @ViewChild('treeComponent') treeComponent;

  private getTreeModel(): TreeModel {
    let model = {
      static: true,
      value: ``,
      settings: {
        cssClasses: {
          'expanded': 'fa fa-caret-down fa-lg',
          'collapsed': 'fa fa-caret-right fa-lg',
          'leaf': 'fa fa-lg',
          'empty': 'fa fa-caret-right disabled'
        },
        templates: {
          'node': `<span class="label label-info"><i class= "fa fa-building" ></i></span>`,
          'leaf': `<span class="label label-primary"><i class= "fa fa-address-card-o" ></i></span>`,
          'leftMenu': `<i class="fa fa-bars"></i>`
        },
        menuItems: [
          { action: NodeMenuItemAction.Custom, name: 'View Details', cssClass: 'fa fa-address-card-o' },
          { action: NodeMenuItemAction.Custom, name: 'Add Child', cssClass: 'fa fa-plus' },
          { action: NodeMenuItemAction.Custom, name: 'Remove', cssClass: 'fa fa-trash-o' }
        ]
      }
    }

    return model;
  };

  private getChildTreeModel(asset: AssetTreeViewModel): TreeModel {
    let model = this.getTreeModel();
    model.value = `${asset.AssetNumber} - ${asset.Name}`;
    model.id = asset.Id;
    if (asset.Children) {
      model.loadChildren = (callback) => {
        this.assetService.GetAssetTree(asset.Id).toPromise().then(childAsset => {
          let assets = childAsset.Result.map(a => {
            return this.getChildTreeModel(a);
          });
          callback(assets);
        });
      };
    }
    return model;
  };

  constructor(
    private modalService: BsModalService,
    private assetService: AssetService,
    private alertService: AlertService,
    private router: Router,
  ) {
  }

  ngOnInit() {
    this.assetService.GetAssetTree().toPromise().then(results => {
      this.assets = results.Result.map(asset => {
        return this.getChildTreeModel(asset);
      });
      this.tree = this.getTreeModel();
      this.tree.children = this.assets;
    });
  }

  createAsset() {
    this.bsModalRef = this.modalService.show(CreateModalComponent);
  }

  onMenuItemSelected(e) {

    let id = e.node.node.id;

    switch (e.selectedItem) {
      case 'Add Child': {
        this.addAsset(id, e.node.node.value);
        break;
      }
      case 'Remove': {
        this.removeAsset(id);
        break;
      }
      case 'View Details': {
        this.router.navigate(['/asset/details', id]);
        break;
      }
      default: {
        break;
      }
    }
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
          this.alertService.showSuccessMessage('Asset removed');
          this.ngOnInit();
        }
      }, e => {
        this.alertService.showErrorMessage();
      });
    }, true);
  }

  handleSelected(e) {
    this.selectedId = e.node.node.id;
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

  handleNextLevel(e) {
    console.log(e);
  }
}
