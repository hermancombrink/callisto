import { Component, OnInit, Input, Output, EventEmitter, AfterViewInit } from '@angular/core';
import { AssetFinanceViewModel } from '../models/assetViewModel';
import { LookupService } from '../../core/lookup.service';
import { DatasourceFactoryService } from '../../core/datasource-factory.service';
import { LookupViewModel } from '../../core/models/lookupViewModel';

@Component({
  selector: 'app-finance',
  templateUrl: './finance.component.html',
  styleUrls: ['./finance.component.css']
})
export class FinanceComponent implements OnInit, AfterViewInit {

  finmodel: AssetFinanceViewModel;
  model: AssetFinanceViewModel;

  @Input()
  assetId: string;
  @Input()
  assetName: string;
  @Input()
  active: boolean;

  lkDepreciationType: LookupViewModel[];

  constructor(private lookupService: LookupService,
    private datasourceFactory: DatasourceFactoryService) {
  }

  ngOnInit() {
    this.lookupService.GetLookupData("depreciationtype").subscribe(c => this.lkDepreciationType = c.Result);
  }

  initComponent(model: AssetFinanceViewModel) {
    this.model = model;
  }

  ngAfterViewInit() {
  }
}

