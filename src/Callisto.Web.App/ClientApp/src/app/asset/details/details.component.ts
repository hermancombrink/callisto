import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AssetViewModel } from '../models/assetViewModel';
import { AssetService } from '../asset.service';
import { AlertService, MessageSeverity } from '../../core/alert.service';
import { RequestStatus } from '../../core/models/requestStatus';
import { Location } from '@angular/common';
import { FileSelectDirective, FileDropDirective, FileUploader } from 'ng2-file-upload/ng2-file-upload';
import { environment } from '../../../environments/environment';
import { AuthService } from '../../core/auth.service';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit, OnDestroy {

  id: string;
  private sub: any;
  model: AssetViewModel = new AssetViewModel();

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private assetService: AssetService,
    private alertService: AlertService,
    private authService: AuthService,
    private location: Location
  ) { }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
      this.id = params['id'];

      this.assetService.GetAsset(this.id).subscribe(c => {
        if (c.Status != RequestStatus.Success) {
          this.alertService.showWarningMessage(c.FriendlyMessage);
        }
        else {
          this.model = c.Result;
        }
      }, e => {
        this.alertService.showErrorMessage('Failed to load asset details');
      });

      this.uploader = new FileUploader({
        url: `${environment.apiUrl}asset/pic/${this.id}`,
        autoUpload: true,
        authToken: this.authService.authToken
      });
    });
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  onSubmit() {
    this.assetService.SaveAsset(this.model).subscribe(c => {
      if (c.Status != RequestStatus.Success) {
        this.alertService.showWarningMessage(c.FriendlyMessage);
      }
      else {
        this.model = c.Result;
        this.alertService.showSuccessMessage('Asset saved');
      }
    }, e => {
      this.alertService.showErrorMessage();
    });
  }

  public uploader: FileUploader;

  public hasBaseDropZoneOver: boolean = false;
  public fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }
}
