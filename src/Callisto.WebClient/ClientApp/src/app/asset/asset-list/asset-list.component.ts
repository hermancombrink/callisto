import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { AssetListItem } from '../models/asset-list-item';
import { TreeModule, TreeModel, NodeMenuItemAction, MenuItemSelectedEvent } from 'ng2-tree';
import { AssetService } from '../services/asset.service';

declare var $: any;

@Component({
    selector: 'app-asset-list',
    templateUrl: './asset-list.component.html',
    styleUrls: ['./asset-list.component.css']
})
export class AssetListComponent implements OnInit, AfterViewInit {
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
        children: this.assetService.getAssetTree()
    };

    constructor(private assetService: AssetService) { }

    ngOnInit() {
    }

    ngAfterViewInit(): void {
    }

    onMenuItemSelected(e: MenuItemSelectedEvent) {
    }
}
