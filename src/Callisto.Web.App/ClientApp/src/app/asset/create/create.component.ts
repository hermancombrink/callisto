import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { AssetAddViewModel } from '../models/AssetAddViewModel';
import { AssetService } from '../asset.service';
import { RequestStatus } from '../../core/models/requestStatus';
import { MessageSeverity, AlertService } from '../../core/alert.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {

  private context: string = 'Add Asset';
  model: AssetAddViewModel = new AssetAddViewModel();

  constructor(
    private location: Location,
    private router: Router,
    private assetService: AssetService,
    private alertService: AlertService
  ) { }

  ngOnInit() {
  }

  onSubmit() {
    this.assetService.AddAsset(this.model).subscribe(c => {
      if (c.status != RequestStatus.Success) {
        this.alertService.showMessage(this.context, `${c.friendlyMessage}`, MessageSeverity.warn);
      }
      else {
        this.alertService.showMessage(this.context, `${this.model.Name} created`, MessageSeverity.info);
        this.router.navigate(['/asset']);
      }
    }, e => {
      this.alertService.showMessage(this.context, `Oops.. That was not suppose to happen`, MessageSeverity.error);
    });
  }

  onCancel() {
    this.location.back();
  }
}
