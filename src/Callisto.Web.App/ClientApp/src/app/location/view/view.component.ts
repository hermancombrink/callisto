import { Component, OnInit } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap';
import { AlertService } from '../../core/alert.service';
import { Router } from '@angular/router';
import { LocationViewModel } from '../models/locationViewModel';
import { LocationService } from '../location.service';
import { RequestStatus } from '../../core/models/requestStatus';

@Component({
  selector: 'app-view',
  templateUrl: './view.component.html',
  styleUrls: ['./view.component.css']
})
export class ViewComponent implements OnInit {

  locations: LocationViewModel[] = []

  constructor(
    private modalService: BsModalService,
    private locationService: LocationService,
    private alertService: AlertService,
    private router: Router) { }

  ngOnInit() {
    this.locationService.GetLocations().subscribe(c => {
      if (c.Status === RequestStatus.Success) {
        this.locations = c.Result;
      } else {
        this.alertService.showWarningMessage(c.FriendlyMessage);
      }
    }, err => this.alertService.showErrorMessage());
  }

}
