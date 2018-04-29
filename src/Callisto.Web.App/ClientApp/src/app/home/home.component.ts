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
      { cols: 2, rows: 1, y: 0, x: 0 },
      { cols: 2, rows: 2, y: 0, x: 2 },
      { cols: 1, rows: 1, y: 0, x: 4 },
      { cols: 1, rows: 1, y: 2, x: 5 },
      { cols: undefined, rows: undefined, y: 1, x: 0 },
      { cols: 1, rows: 1, y: undefined, x: undefined },
      { cols: 2, rows: 2, y: 3, x: 5 },
      { cols: 2, rows: 2, y: 2, x: 0 },
      { cols: 2, rows: 1, y: 2, x: 2 },
      { cols: 1, rows: 1, y: 2, x: 4 },
      { cols: 1, rows: 1, y: 2, x: 6 }
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
