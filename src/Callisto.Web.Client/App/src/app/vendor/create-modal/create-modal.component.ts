import { Component, OnInit, ViewChild } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';
import { Router } from '@angular/router';
import { AlertService } from '../../core/alert.service';
import { DxFormComponent } from 'devextreme-angular';
import { AddVendorViewModel } from '../models/VendorViewModels';
import { VendorService } from '../Vendor.service';
import { RequestStatus } from '../../core/models/requestStatus';

@Component({
  selector: 'app-create-modal',
  templateUrl: './create-modal.component.html',
  styleUrls: ['./create-modal.component.css']
})
export class CreateModalComponent implements OnInit {

  model: AddVendorViewModel = new AddVendorViewModel();
  @ViewChild('dxForm') dxForm: DxFormComponent;

  constructor(public bsModalRef: BsModalRef,
    private router: Router,
    private alertService: AlertService,
    private vendorService: VendorService) { }

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

    this.vendorService.AddVendorMember(this.model).subscribe(c => {
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
