import { Component, OnInit, Input } from '@angular/core';
import { AssetInspectionViewModel } from '../models/assetViewModel';

@Component({
  selector: 'app-inspection',
  templateUrl: './inspection.component.html',
  styleUrls: ['./inspection.component.css']
})
export class InspectionComponent implements OnInit {

  model: AssetInspectionViewModel[];

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
