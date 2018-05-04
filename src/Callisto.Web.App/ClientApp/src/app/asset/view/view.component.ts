import { Component, OnInit, ViewChild } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { TreeModule, TreeModel, NodeMenuItemAction, MenuItemSelectedEvent } from 'ng2-tree';
import { CreateModalComponent } from '../create-modal/create-modal.component';
import { AssetService } from '../asset.service';

@Component({
  selector: 'app-view',
  templateUrl: './view.component.html',
  styleUrls: ['./view.component.css']
})
export class ViewComponent implements OnInit {
  bsModalRef: BsModalRef;

  @ViewChild('treeComponent') treeComponent;

  tree: TreeModel = {
    value: '',
    settings: {
      'rightMenu': true,
      'leftMenu': true,
      'cssClasses': {
        'expanded': 'fa fa-caret-down fa-lg',
        'collapsed': 'fa fa-caret-right fa-lg',
        'leaf': 'fa fa-lg',
        'empty': 'fa fa-caret-right disabled'
      },
      'templates': {
        'node': `<span class="label label-info"><i class= "fa fa-building" ></i></span>`,
        'leaf': `<span class="label label-primary"><i class= "fa fa-bolt" ></i></span>`,
        'leftMenu': `<i class="fa fa-bars"></i>`
      },
      'menuItems': [
        { action: NodeMenuItemAction.Custom, name: 'View Details', cssClass: 'fa fa-address-card-o' },
        { action: NodeMenuItemAction.Custom, name: 'Rename', cssClass: 'fa fa-recycle' },
        { action: NodeMenuItemAction.Custom, name: 'Add Child', cssClass: 'fa fa-plus' },
        { action: NodeMenuItemAction.Custom, name: 'Remove', cssClass: 'fa fa-trash-o' }
      ]
    },
    children: this.assetService.GetAssetTree()
  };


  constructor(
    private modalService: BsModalService,
    private assetService: AssetService)
  {
  }

  ngOnInit() {
  }

  createAsset() {
    const initialState = {
      list: [
        'Open a modal with component',
        'Pass your data',
        'Do something else',
        '...'
      ],
      title: 'Modal with component'
    };
    this.bsModalRef = this.modalService.show(CreateModalComponent, { initialState });
    this.bsModalRef.content.closeBtnName = 'Close';
  }

}
