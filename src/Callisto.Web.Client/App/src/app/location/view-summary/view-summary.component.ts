import { Component, OnInit, Input, DoCheck, ViewChild, Output, EventEmitter } from '@angular/core';
import { LocationViewModel } from '../models/locationViewModel';
import { LocationComponent } from '../location.component';
import { AlertService, MessageSeverity } from '../../core/alert.service';
import { LocationService } from '../location.service';
import { RequestStatus } from '../../core/models/requestStatus';

@Component({
  selector: 'app-view-summary',
  templateUrl: './view-summary.component.html',
  styleUrls: ['./view-summary.component.css']
})
export class ViewSummaryComponent implements OnInit, DoCheck {

  @Input() location: LocationViewModel;
  @Output() outDelete: EventEmitter<any> = new EventEmitter();

  @ViewChild('locPanel') locationPanel: LocationComponent;

  constructor(
    private alertService: AlertService,
    private locationService: LocationService
  ) { }

  ngOnInit() {
    this.locationPanel.initAutoComplete();
  }

  ngDoCheck(): void {
    if (this.locationPanel.model.Id !== this.location.Id) {
      this.locationPanel.initLocation(this.location);
    }
  }

  onSubmit() {
    this.alertService.showDialog('Do you want to update this location?', 'Are you sure?', MessageSeverity.warn, c => {
      this.locationService.SaveLocation(this.location).subscribe(x => {
        if (x.Status !== RequestStatus.Success) {
          this.alertService.showWarningMessage(x.FriendlyMessage);
        } else {
          this.outDelete.emit(null);
          this.alertService.showSuccessMessage('Location has been updated');
        }
      }, e => this.alertService.showErrorMessage())
    });
  }

  removeLocation() {
    this.alertService.showDialog('Do you want to remove this location?', 'Are you sure?', MessageSeverity.warn, c => {
      this.locationService.RemoveLocation(this.location.Id).subscribe(x => {
        if (x.Status !== RequestStatus.Success) {
          this.alertService.showWarningMessage(x.FriendlyMessage);
        } else {
          this.outDelete.emit(null);
          this.alertService.showSuccessMessage('Location has been removed');
        }
      }, e => this.alertService.showErrorMessage())
    });
  }
}
