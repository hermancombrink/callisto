import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AssetViewModel } from '../models/assetViewModel';
import { AssetService } from '../asset.service';
import { AlertService } from '../../core/alert.service';
import { RequestStatus } from '../../core/models/requestStatus';

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
    private alertService: AlertService
  ) { }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
      this.id = params['id'];
      console.info(this.id);

      this.assetService.GetAsset(this.id).subscribe(c => {
        if (c.status != RequestStatus.Success) {
          console.error(c.friendlyMessage);
        }
        else {
          this.model = c.result;
          console.log(this.model);
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
  }


}
