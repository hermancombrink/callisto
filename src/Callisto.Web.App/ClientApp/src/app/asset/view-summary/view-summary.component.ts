import { Component, OnInit, Input, DoCheck, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { AlertService } from '../../core/alert.service';
import { AssetViewModel } from '../models/assetViewModel';
import { AssetService } from '../asset.service';
import { LocationComponent } from '../../location/location.component';
import { RequestStatus } from '../../core/models/requestStatus';

@Component({
  selector: 'app-view-summary',
  templateUrl: './view-summary.component.html',
  styleUrls: ['./view-summary.component.css']
})
export class ViewSummaryComponent implements OnInit, DoCheck {
  @Input() id: string;
  @ViewChild('location') locationPanel: LocationComponent;

  currentAsset: AssetViewModel;
  private _currentId: string;

  constructor(
    private assetService: AssetService,
    private alertService: AlertService,
    private router: Router,
  ) { }

  ngOnInit() {
  }

  ngDoCheck(): void {
    if (this.id && (!this._currentId || this.id !== this._currentId)) {
      this._currentId = this.id;
      this.loadAsset();
    }
  }
  onDetails() {
    this.router.navigate(['/asset/details', this.currentAsset.Id]);
  }

  loadAsset() {
  this.assetService.GetAsset(this.id).subscribe(c => {
    if (c.Status === RequestStatus.Success) {
      this.currentAsset = c.Result;
      this.locationPanel.initLocation({
        Longitude : c.Result.Longitude,
        Latitude : c.Result.Latitude,
        FormattedAddress : c.Result.FormattedAddress
      });
    } else {
      this.alertService.showWarningMessage(c.FriendlyMessage);
    }
  }, err => this.alertService.showErrorMessage());
  }
}
