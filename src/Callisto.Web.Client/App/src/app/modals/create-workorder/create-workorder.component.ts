import { Component, OnInit, ViewChild } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';
import { DxFormComponent } from 'devextreme-angular';
import { BaseWorkOrderViewModel } from '../../workorder/models/WorkOrderViewModel';
import { WorkerorderService } from '../../workorder/workerorder.service';
import { RequestStatus } from '../../core/models/requestStatus';
import { AlertService } from '../../core/alert.service';

@Component({
  selector: 'app-create-workorder',
  templateUrl: './create-workorder.component.html',
  styleUrls: ['./create-workorder.component.css']
})
export class CreateWorkorderComponent implements OnInit {

  assetId: string;
  assetName: string;
  @ViewChild('dxForm') dxForm: DxFormComponent;
  model: BaseWorkOrderViewModel = new BaseWorkOrderViewModel();

  constructor(
    public bsModalRef: BsModalRef,
    private workService: WorkerorderService,
    private alertService: AlertService
  ) { }

  ngOnInit() {
    this.model.AssetId = this.assetId;
  }

  onSubmit() {
    let isvalid = this.dxForm.instance.validate();
    if (!isvalid.isValid) {
      return;
    }

    this.workService.AddWorkOrder(this.model).subscribe(c => {
      if (c.Status !== RequestStatus.Success) {
        this.alertService.showWarningMessage(`${c.FriendlyMessage}`);
      } else {
        this.alertService.showSuccessMessage(`Work order created`);
        this.bsModalRef.hide();
      }
    }, e => {
      this.alertService.showErrorMessage();
    });
  }

  onCancel() {
    this.bsModalRef.hide();
  }

}
