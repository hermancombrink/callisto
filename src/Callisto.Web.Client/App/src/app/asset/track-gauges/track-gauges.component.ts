import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-track-gauges',
  templateUrl: './track-gauges.component.html',
  styleUrls: ['./track-gauges.component.css']
})
export class TrackGaugesComponent implements OnInit {

  @Input()
  assetId: string;

  constructor() { }

  ngOnInit() {
  }

}
