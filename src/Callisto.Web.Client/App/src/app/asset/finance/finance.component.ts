import { Component, OnInit, Input } from '@angular/core';
import { AssetFinanceViewModel } from '../models/assetViewModel';

@Component({
  selector: 'app-finance',
  templateUrl: './finance.component.html',
  styleUrls: ['./finance.component.css']
})
export class FinanceComponent implements OnInit {

  @Input()
  model: AssetFinanceViewModel = new AssetFinanceViewModel();

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
