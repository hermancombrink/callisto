import { Component, OnInit, ChangeDetectionStrategy, ViewEncapsulation } from '@angular/core';
import { GridsterConfig, GridsterItem, GridType, DisplayGrid, CompactType } from 'angular-gridster2';
import { GridOptions } from './GridOptions';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.None
})
export class HomeComponent implements OnInit {

  options: GridsterConfig = GridOptions.GridsterConfig;
  reports: Array<GridsterItem> = [];

  ngOnInit(): void {
    this.reports = [
    ];
  }

  removeItem($event, item) {
    $event.preventDefault();
    $event.stopPropagation();
    this.reports.splice(this.reports.indexOf(item), 1);
  }

  addItem() {
    this.reports.push({});
  }

}
