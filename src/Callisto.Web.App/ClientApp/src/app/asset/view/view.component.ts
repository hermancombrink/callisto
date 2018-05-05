import { Component, OnInit, ViewChild } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { TreeModule, TreeModel, NodeMenuItemAction, MenuItemSelectedEvent, Tree } from 'ng2-tree';
import { CreateModalComponent } from '../create-modal/create-modal.component';
import { AssetService } from '../asset.service';
import { TreeStatus, Ng2TreeSettings } from 'ng2-tree/src/tree.types';
import { Subject } from 'rxjs';
import { Router } from '@angular/router';
import { AlertService } from '../../core/alert.service';

@Component({
  selector: 'app-view',
  templateUrl: './view.component.html',
  styleUrls: ['./view.component.css']
})
export class ViewComponent implements OnInit {
  bsModalRef: BsModalRef;

  assets: TreeModel[];

  @ViewChild('treeComponent') treeComponent;

  tree: TreeModel;

  constructor(
    private modalService: BsModalService,
    private assetService: AssetService,
    private alertService: AlertService,
    private router: Router,
  ) {
  }

  ngOnInit() {
    this.assetService.GetTopLevelAssets().subscribe(results => {
      this.assets = results.Result.map(asset => {
        return {
          id: asset.Id,
          value: `${asset.AssetNumber} - ${asset.Name}`,
          emitLoadNextLevel: true
        }
      });
      this.tree = {
        value: '',
        settings: {
          isCollapsedOnInit: false,
          rightMenu: true,
          leftMenu: true,
          cssClasses: {
            'expanded': 'fa fa-caret-down fa-lg',
            'collapsed': 'fa fa-caret-right fa-lg',
            'leaf': 'fa fa-lg',
            'empty': 'fa fa-caret-right disabled'
          },
          templates: {
            'node': `<span class="label label-info"><i class= "fa fa-building" ></i></span>`,
            'leaf': `<span class="label label-primary"><i class= "fa fa-bolt" ></i></span>`,
            'leftMenu': `<i class="fa fa-bars"></i>`
          },
          menuItems: [
            { action: NodeMenuItemAction.Custom, name: 'View Details', cssClass: 'fa fa-address-card-o' },
            { action: NodeMenuItemAction.Custom, name: 'Add Child', cssClass: 'fa fa-plus' },
            { action: NodeMenuItemAction.Custom, name: 'Remove', cssClass: 'fa fa-trash-o' }
          ]
        },
        children: this.assets
      };
    });
  }

  createAsset() {
    this.bsModalRef = this.modalService.show(CreateModalComponent);
  }

  onMenuItemSelected(e) {
    console.log(e);
    switch (e.selectedItem) {
      case 'Add Child': {
        const initialState = {
          parentId: e.node.node.id,
          parentName: e.node.node.value
        };
        this.bsModalRef = this.modalService.show(CreateModalComponent, { initialState });
        break;
      }
      case 'Remove': {
        this.alertService.showInfoMessage('Remove Item');
        break;
      }
      case 'View Details': {
        this.router.navigate(['/asset/details', e.node.node.id]);
        break;
      }
      default: {
        this.alertService.showInfoMessage('Load children');
        break;
      }
    }
  }

  handleNextLevel(e) {
    console.log(e);
  }
}
