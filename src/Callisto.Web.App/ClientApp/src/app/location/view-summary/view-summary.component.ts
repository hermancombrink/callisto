import { Component, OnInit, Input, DoCheck, ViewChild } from '@angular/core';
import { LocationViewModel } from '../models/locationViewModel';
import { LocationComponent } from '../location.component';
import { AlertService, MessageSeverity } from '../../core/alert.service';

@Component({
  selector: 'app-view-summary',
  templateUrl: './view-summary.component.html',
  styleUrls: ['./view-summary.component.css']
})
export class ViewSummaryComponent implements OnInit, DoCheck {

  @Input() location: LocationViewModel;
  @ViewChild('locPanel') locationPanel: LocationComponent;

  constructor(
    private alertService: AlertService
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
    this.alertService.showDialog('Do you want to update this location?', 'Are you sure?', MessageSeverity.warn, null);
  }

  removeLocation() {
    this.alertService.showDialog('Do you want to remove this location?', 'Are you sure?', MessageSeverity.warn, null);
  }

}
