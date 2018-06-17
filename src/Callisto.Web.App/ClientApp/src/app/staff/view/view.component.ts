import { Component, OnInit, OnDestroy } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { AlertService, MessageSeverity } from '../../core/alert.service';
import { Router } from '@angular/router';
import { CreateModalComponent } from '../create-modal/create-modal.component';
import { StaffService } from '../staff.service';
import { RequestStatus } from '../../core/models/requestStatus';
import { StaffViewModel } from '../models/staffViewModels';
import { ISubscription } from 'rxjs/Subscription';

@Component({
  selector: 'app-view',
  templateUrl: './view.component.html',
  styleUrls: ['./view.component.css']
})
export class ViewComponent implements OnInit, OnDestroy {

  bsModalRef: BsModalRef;
  members: StaffViewModel[] = [];
  modalSub: ISubscription;

  constructor(private modalService: BsModalService,
    private alertService: AlertService,
    private staffService: StaffService,
    private router: Router) {
  }

  ngOnInit() {
    this.modalSub = this.modalService.onHidden.subscribe(c => this.loadMembers());
    this.loadMembers();
  }

  ngOnDestroy(): void {
    this.modalSub.unsubscribe();
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

  handleRemove(e) {
    this.removeMember(e.data.Id);
  }

  removeMember(id: string) {
    this.alertService.showDialog('Do you want to remove this item?', 'Are you sure?', MessageSeverity.warn, x => {
      this.staffService.RemoveStaffMember(id).subscribe(c => {
        if (c.Status !== RequestStatus.Success) {
          this.alertService.showWarningMessage(c.FriendlyMessage);
        } else {
          this.loadMembers();
          this.alertService.showSuccessMessage('Member removed');
        }
      }, e => {
        this.alertService.showErrorMessage();
      });
    }, true);
  }
}
