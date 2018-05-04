import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AssetViewModel } from '../models/assetViewModel';
import { AssetService } from '../asset.service';
import { AlertService, MessageSeverity } from '../../core/alert.service';
import { RequestStatus } from '../../core/models/requestStatus';
import { Location } from '@angular/common';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit, OnDestroy {
  context: string = 'Asset Details';
  id: string;
  private sub: any;
  model: AssetViewModel = new AssetViewModel();

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private assetService: AssetService,
    private alertService: AlertService,
    private location: Location
  ) { }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
      this.id = params['id'];
      console.info(this.id);

      this.assetService.GetAsset(this.id).subscribe(c => {
        if (c.Status != RequestStatus.Success) {
          console.error(c.FriendlyMessage);
        }
        else {
          this.model = c.Result;
        }
      }, e => {
        console.error(e);
      });
    });
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  onSubmit() {
    this.assetService.SaveAsset(this.model).subscribe(c => {
      if (c.Status != RequestStatus.Success) {
        console.error(c.FriendlyMessage);
      }
      else {
        this.model = c.Result;
        this.alertService.showMessage(this.context, 'Asset saved', MessageSeverity.success);
      }
    }, e => {
      console.error(e);
    });
  }


}
