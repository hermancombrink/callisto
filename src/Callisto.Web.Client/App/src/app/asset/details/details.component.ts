import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AssetDetailViewModel, AssetTreeViewModel } from '../models/assetViewModel';
import { AssetService } from '../asset.service';
import { AlertService, MessageSeverity } from '../../core/alert.service';
import { RequestStatus } from '../../core/models/requestStatus';
import { Location } from '@angular/common';
import { FileUploader } from 'ng2-file-upload/ng2-file-upload';
import { environment } from '../../../environments/environment';
import { AuthService } from '../../core/auth.service';
import { RequestResult } from '../../core/models/requestResult';
import { LocationComponent } from '../../location/location.component';
import { DxFormComponent, DxTreeViewComponent } from 'devextreme-angular';
import { BsModalRef, BsModalService, TabsetComponent } from 'ngx-bootstrap';
import { ISubscription } from 'rxjs/Subscription';
import { ListWorkOrderComponent } from '../list-workorder/list-workorder.component';
import { ListMainScheduleComponent } from '../list-main-schedule/list-main-schedule.component';
import { FinanceComponent } from '../finance/finance.component';
import { InspectionComponent } from '../inspection/inspection.component';
import { LookupViewModel } from '../../core/models/lookupViewModel';
import { LookupService } from '../../core/lookup.service';
import { DatasourceFactoryService } from '../../core/datasource-factory.service';
import DataSource from "devextreme/data/data_source";
import { UploadPicComponent } from '../../documents/upload-pic/upload-pic.component';
import { SearchComponent } from '../../location/search/search.component';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit, OnDestroy {

  id: string;
  private sub: any;
  model: AssetDetailViewModel = new AssetDetailViewModel();

  @ViewChild('location') locationPanel: SearchComponent;
  @ViewChild('dxForm') dxForm: DxFormComponent;
  @ViewChild('staticTabs') tabs: TabsetComponent;

  @ViewChild('workOrders') tabWorkOrders: ListWorkOrderComponent;
  @ViewChild('scheduleMain') tabScheduleMain: ListMainScheduleComponent;
  @ViewChild('finance') tabFinance: FinanceComponent;
  @ViewChild('inspection') tabInspection: InspectionComponent;

  @ViewChild(DxTreeViewComponent) treeView;

  mapVisible = false;
  parentTree: AssetTreeViewModel[];
  lkReadingType: LookupViewModel[];
  lkStatusType: LookupViewModel[];
  lkCriticalType: LookupViewModel[];

  dblkManufacturer: DataSource;
  dblkDepartment: DataSource;
  dblkCustomer: DataSource;
  dblkContractor: DataSource;
  dblkTags: DataSource;

  bsModalRef: BsModalRef;
  modalSub: ISubscription;

  constructor(
    private route: ActivatedRoute,
    private assetService: AssetService,
    private alertService: AlertService,
    public _location: Location,
    private modalService: BsModalService,
    private lookupService: LookupService,
    private datasourceFactory: DatasourceFactoryService
  ) {
    this.dblkTags = this.datasourceFactory.GetTagLookup("tags");

    this.dblkManufacturer = this.datasourceFactory.GetAutoCompleteLookup("manufacturer");
    this.dblkDepartment = this.datasourceFactory.GetAutoCompleteLookup("department");
    this.dblkCustomer = this.datasourceFactory.GetAutoCompleteLookup("customer");
    this.dblkContractor = this.datasourceFactory.GetAutoCompleteLookup("contractor");
  }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
      this.id = params['id'];

      this.assetService.GetAssetDetail(this.id).subscribe(c => {
        if (c.Status !== RequestStatus.Success) {
          this.alertService.showWarningMessage(c.FriendlyMessage);
        } else {
          this.model = c.Result;
          this.assetService.GetAssetTreeParents(this.id).subscribe(t => {
            this.parentTree = t.Result;
          });
        }
      }, e => {
        this.alertService.showErrorMessage('Failed to load asset details');
      });

    });

    this.lookupService.GetLookupData("readingtype").subscribe(x => { this.lkReadingType = x.Result; });
    this.lookupService.GetLookupData("statustype").subscribe(x => { this.lkStatusType = x.Result; });
    this.lookupService.GetLookupData("criticaltype").subscribe(x => { this.lkCriticalType = x.Result; });

    this.modalSub = this.modalService.onHidden.subscribe(c => {
      this.tabs.tabs[0].active = true; // reset work orders to active tab
    });
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
    this.modalSub.unsubscribe();

  }

  onSubmit() {
    let isvalid = this.dxForm.instance.validate();
    if (!isvalid.isValid) {
      return;
    }
    this.model.LocationData = this.locationPanel.getModel();
    this.model.ParentId = this.model.ParentId && this.model.ParentId !== '0' ? this.model.ParentId : null;
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

  onTabChange(tab: string){
    switch (tab)
    {
      case 'work' :
      {
        this.tabWorkOrders.refresh();
        break;
      }
      case 'schedule' :
      {
        this.tabScheduleMain.refresh();
        break;
      }
      case 'finance' :
      {
        this.tabFinance.initComponent(this.model.FinanceData);
        break;
      }
      case 'inspection' :
      {
        this.tabInspection.refresh();
        break;
      }
      case 'location' :
      {
        this.locationPanel.initComponent(this.model.LocationData);
        break;
      }
    };
  }
}

