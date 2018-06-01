import { Component, OnInit } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { AlertService } from '../../core/alert.service';
import { Router } from '@angular/router';
import { CreateModalComponent } from '../create-modal/create-modal.component';
import { StaffService } from '../staff.service';
import { RequestStatus } from '../../core/models/requestStatus';
import { StaffViewModel } from '../models/staffViewModels';

@Component({
  selector: 'app-view',
  templateUrl: './view.component.html',
  styleUrls: ['./view.component.css']
})
export class ViewComponent implements OnInit {

  bsModalRef: BsModalRef;
  members: StaffViewModel[] = [];

  constructor(private modalService: BsModalService,
    private alertService: AlertService,
    private staffService: StaffService,
    private router: Router) {
    this.modalService.onHidden.subscribe(c => this.loadMembers());
  }

  ngOnInit() {
    this.loadMembers();
  }

  createMember() {
    this.bsModalRef = this.modalService.show(CreateModalComponent);
  }

  loadMembers() {
    this.staffService.GetStaffMembers().subscribe(c => {
      if (c.Status === RequestStatus.Success) {
        this.members = c.Result;
      } else {
        this.alertService.showWarningMessage(c.FriendlyMessage);
      }
    }, err => this.alertService.showErrorMessage());
  }
}
