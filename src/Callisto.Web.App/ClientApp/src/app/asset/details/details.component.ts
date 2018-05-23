import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AssetDetailViewModel, AssetTreeViewModel } from '../models/assetViewModel';
import { AssetService } from '../asset.service';
import { AlertService, MessageSeverity } from '../../core/alert.service';
import { RequestStatus } from '../../core/models/requestStatus';
import { Location } from '@angular/common';
import { FileSelectDirective, FileDropDirective, FileUploader } from 'ng2-file-upload/ng2-file-upload';
import { environment } from '../../../environments/environment';
import { AuthService } from '../../core/auth.service';
import { HttpHeaders } from '@angular/common/http';
import { RequestResult } from '../../core/models/requestResult';
import { LocationComponent } from '../../location/location.component';
import { CacheService } from '../../core/cache.service';
import { assetConstants } from '../models/constants';
import { FormControl } from '@angular/forms';
import { DxFormComponent, DxTreeViewComponent } from 'devextreme-angular';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit, OnDestroy {

  id: string;
  private sub: any;
  model: AssetDetailViewModel = new AssetDetailViewModel();

  @ViewChild('location') locationPanel: LocationComponent;
  @ViewChild('dxForm') dxForm: DxFormComponent;
  @ViewChild(DxTreeViewComponent) treeView;

  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  mapVisible = false;
  parentTree: AssetTreeViewModel[];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private assetService: AssetService,
    private alertService: AlertService,
    private authService: AuthService,
    public _location: Location
  ) {
  }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
      this.id = params['id'];
      this.setupAsset();
      this.setUploader();
    });
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  onSubmit() {
    let isvalid = this.dxForm.instance.validate();
    if (!isvalid.isValid) {
      return;
    }
    this.model.Location = this.locationPanel.model;
    this.model.ParentId = this.model.ParentId ? this.model.ParentId[0] : null;
    this.assetService.SaveAsset(this.model).subscribe(c => {
      if (c.Status !== RequestStatus.Success) {
        this.alertService.showWarningMessage(c.FriendlyMessage);
      } else {
        this.alertService.showSuccessMessage('Asset saved');
      }
    }, e => {
      this.alertService.showErrorMessage();
    });
  }

  fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  showMap() {
    this.mapVisible = !this.mapVisible;
    if (this.mapVisible) {
      this.locationPanel.draw();
    }
  }

  removeAsset() {
    this.alertService.showDialog('Do you want to remove this item?', 'Are you sure?', MessageSeverity.warn, x => {
      this.assetService.RemoveAsset(this.id).subscribe(c => {
        if (c.Status !== RequestStatus.Success) {
          this.alertService.showWarningMessage(c.FriendlyMessage);
        } else {
          this.alertService.showSuccessMessage('Asset removed');
          this._location.back();
        }
      }, e => {
        this.alertService.showErrorMessage();
      });
    }, true);
  }

  private setupAsset() {
    this.assetService.GetAssetDetail(this.id).subscribe(c => {
      if (c.Status !== RequestStatus.Success) {
        this.alertService.showWarningMessage(c.FriendlyMessage);
      } else {
        this.model = c.Result;
        this.locationPanel.initAutoComplete();
        this.locationPanel.initLocation(this.model.Location);

        this.assetService.GetAssetTreeParents(this.id).subscribe(t => {
          this.parentTree = t.Result;
        });
      }
    }, e => {
      this.alertService.showErrorMessage('Failed to load asset details');
    });
  }

  private setUploader() {
    this.uploader = new FileUploader({
      url: `${environment.apiUrl}asset/pic/${this.id}`,
      autoUpload: true,
      authToken: this.authService.authToken
    });
    this.uploader.onCompleteItem = (item: any, response: any, status: any, headers: any) => {
      let result = <RequestResult>JSON.parse(response)
      if (status === 200 && result.Status === RequestStatus.Success) {
        this.model.PictureUrl = result.Result;
      } else {
        this.alertService.showWarningMessage(result.FriendlyMessage);
      }
    };
  }

  syncTreeViewSelection(e) {
    if (!this.treeView) { return; }

    if (!this.model.ParentId) {
      this.treeView.instance.unselectAll();
    } else {
      this.treeView.instance.selectItem(this.model.ParentId);
    }
  }

  treeView_itemSelectionChanged(e) {
    this.model.ParentId = e.component.getSelectedNodesKeys();
  }
}
