import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';
import { AssetAddViewModel } from '../models/AssetAddViewModel';
import { Router } from '@angular/router';
import { AssetService } from '../asset.service';
import { AlertService, MessageSeverity } from '../../core/alert.service';
import { RequestStatus } from '../../core/models/requestStatus';

@Component({
  selector: 'app-create-modal',
  templateUrl: './create-modal.component.html',
  styleUrls: ['./create-modal.component.css']
})
export class CreateModalComponent implements OnInit {

  context: string = 'Create Asset';
  model: AssetAddViewModel = new AssetAddViewModel();

  constructor(
    public bsModalRef: BsModalRef,
    private router: Router,
    private assetService: AssetService,
    private alertService: AlertService
  ) { }

  ngOnInit() {
  }

  onSubmit() {
    this.assetService.AddAsset(this.model).subscribe(c => {
      if (c.Status != RequestStatus.Success) {
        this.alertService.showMessage(this.context, `${c.FriendlyMessage}`, MessageSeverity.warn);
      }
      else {
        console.info(c.Result);
        this.alertService.showMessage(this.context, `${this.model.Name} created`, MessageSeverity.info);
        this.bsModalRef.hide();
        this.router.navigate(['/asset/details', c.Result]);
      }
    }, e => {
      this.alertService.showMessage(this.context, `Oops.. That was not suppose to happen`, MessageSeverity.error);
    });
  }

  onCancel() {
    this.bsModalRef.hide();
  }
}
