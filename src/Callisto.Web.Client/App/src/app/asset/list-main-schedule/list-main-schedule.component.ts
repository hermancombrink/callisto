import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-list-main-schedule',
  templateUrl: './list-main-schedule.component.html',
  styleUrls: ['./list-main-schedule.component.css']
})
export class ListMainScheduleComponent implements OnInit {

  @Input()
  assetId: string;
  @Input()
  assetName: string;
  @Input()
  active: boolean;

  constructor() { }

  ngOnInit() {
  }

  refresh() {
  }
}
