import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';
import { AssetAddViewModel } from '../models/AssetAddViewModel';
import { Router } from '@angular/router';
import { AssetService } from '../asset.service';
import { AlertService, MessageSeverity } from '../../core/alert.service';
import { RequestStatus } from '../../core/models/requestStatus';
import { CacheService } from '../../core/cache.service';
import { assetConstants } from '../models/constants';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-create-modal',
  templateUrl: './create-modal.component.html',
  styleUrls: ['./create-modal.component.css']
})
export class CreateModalComponent implements OnInit {
  parentId: string;
  parentName: string;
  model: AssetAddViewModel = new AssetAddViewModel();

  constructor(
    public bsModalRef: BsModalRef,
    private router: Router,
    private assetService: AssetService,
    private alertService: AlertService,
    private readonly _cache: CacheService
  ) { }

  ngOnInit() {
    this.model.ParentId = this.parentId;
  }

  onSubmit() {
    this.assetService.AddAsset(this.model).subscribe(c => {
      if (c.Status !== RequestStatus.Success) {
        this.alertService.showWarningMessage(`${c.FriendlyMessage}`);
      } else {
        this.alertService.showSuccessMessage(`${this.model.Name} created`);
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
