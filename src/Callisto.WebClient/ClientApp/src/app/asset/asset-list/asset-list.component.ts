import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { AssetListItem } from '../models/asset-list-item';
import { TreeModule, TreeModel, NodeMenuItemAction, MenuItemSelectedEvent } from 'ng2-tree';

declare var $: any;

@Component({
    selector: 'app-asset-list',
    templateUrl: './asset-list.component.html',
    styleUrls: ['./asset-list.component.css']
})
export class AssetListComponent implements OnInit, AfterViewInit {
    Assets: AssetListItem[];

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
        children: [
            {
                value: 'Object-oriented programming',
                children: [{ value: 'Java' }, { value: 'C++' }, { value: 'C#' }]
            },
            {
                value: 'Prototype-based programming',
                children: [{ value: 'JavaScript' }, { value: 'CoffeeScript' }, { value: 'Lua' }]
            }
        ]
    };

    constructor() { }

    ngOnInit() {
        this.Assets = [];
        const admin = AssetListItem.NewInstance(1, 'ADMIN', 'Administration Building', 'cog');
        const floor1 = AssetListItem.NewInstance(11, 'FLOOR1', '1st Floor Admin Building');
        floor1.Children.push(AssetListItem.NewInstance(111, 'RM101', 'Office No 1 Admin Building 1st Floor'));
        floor1.Children.push(AssetListItem.NewInstance(112, 'RM102', 'Office No 2 Admin Building 1st Floor'));
        floor1.Children.push(AssetListItem.NewInstance(113, 'RM103', 'Office No 3 Admin Building 1st Floor'));
        admin.Children.push(floor1);
        const floor2 = AssetListItem.NewInstance(12, 'FLOOR2', '2nd Floor Admin Building');
        admin.Children.push(floor2);
        floor2.Children.push(AssetListItem.NewInstance(121, 'RM201', 'Room 201 2nd Floor Admin Building'));
        floor2.Children.push(AssetListItem.NewInstance(122, 'RM202', 'Room 202 2nd Floor Admin Building'));
        floor2.Children.push(AssetListItem.NewInstance(123, 'RM203', 'Room 203 2nd Floor Admin Building'));
        floor2.Children.push(AssetListItem.NewInstance(124, 'RM204', 'Computer Room 2nd Floor Admin Building'));
        floor2.Children.push(AssetListItem.NewInstance(125, 'RM205', 'Plant Room 2nd Floor Admin Building'));
        const admin2 = AssetListItem.NewInstance(2, 'ADMIN2', 'Administration Building 2', 'users');
        const BES = AssetListItem.NewInstance(3, 'BES', 'Building & Engineering Services', 'child');
        const PROD = AssetListItem.NewInstance(4, 'PROD', 'Production', 'bolt');
        const WH = AssetListItem.NewInstance(5, 'WH', 'Warehouse', 'laptop');
        const WTP = AssetListItem.NewInstance(6, 'WTP', 'Waste Treatment Plant 1', 'bomb');
        this.Assets.push(
            admin,
            admin2,
            BES,
            PROD,
            WH,
            WTP
        );
    }
    ngAfterViewInit(): void {
    }

    onMenuItemSelected(e: MenuItemSelectedEvent) {
        console.log(e.selectedItem);
    }
}
