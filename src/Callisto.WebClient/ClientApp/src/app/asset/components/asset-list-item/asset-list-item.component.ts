import { Component, OnInit, Input } from '@angular/core';
import { AssetListItem } from '../../models/asset-list-item';

@Component({
  // tslint:disable-next-line:component-selector
  selector: '[asset-list-item]',
  templateUrl: './asset-list-item.component.html',
  styleUrls: ['./asset-list-item.component.css']
})
export class AssetListItemComponent implements OnInit {

  @Input() ListItem: AssetListItem;

  constructor() { }

  ngOnInit() {
  }

}
