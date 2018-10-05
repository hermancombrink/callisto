import { Component, OnInit, ViewChild } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';
import { Router } from '@angular/router';
import { AlertService } from '../../core/alert.service';
import { DxFormComponent } from 'devextreme-angular';
import { AddMemberViewModel } from '../models/memberViewModels';
import { TeamService } from '../team.service';
import { RequestStatus } from '../../core/models/requestStatus';

@Component({
  selector: 'app-create-modal',
  templateUrl: './create-modal.component.html',
  styleUrls: ['./create-modal.component.css']
})
export class CreateModalComponent implements OnInit {

  model: AddMemberViewModel = new AddMemberViewModel();
  @ViewChild('dxForm') dxForm: DxFormComponent;

  constructor(public bsModalRef: BsModalRef,
    private router: Router,
    private alertService: AlertService,
    private teamService: TeamService) { }

  ngOnInit() {
  }

  onCancel() {
    this.bsModalRef.hide();
  }

  onSubmit() {
    let isvalid = this.dxForm.instance.validate();
    if (!isvalid.isValid) {
      return;
    }

    this.teamService.AddTeamMember(this.model).subscribe(c => {
      if (c.Status !== RequestStatus.Success) {
        this.alertService.showWarningMessage(`${c.FriendlyMessage}`);
      } else {
        this.alertService.showSuccessMessage(`${this.model.FirstName} created`);
        this.bsModalRef.hide();
      }
    }, e => {
      this.alertService.showErrorMessage();
    });
  }
}
