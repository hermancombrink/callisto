import { Component, OnInit, Input } from '@angular/core';
import { WorkOrderItemViewModel } from '../../workorder/models/workOrderViewModel';
import { WorkerorderService } from '../../workorder/workerorder.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { ISubscription } from 'rxjs/Subscription';
import { CreateWorkorderComponent } from '../../modals/create-workorder/create-workorder.component';

@Component({
  selector: 'app-list-workorder',
  templateUrl: './list-workorder.component.html',
  styleUrls: ['./list-workorder.component.css']
})
export class ListWorkOrderComponent implements OnInit {

  workOrders: WorkOrderItemViewModel[] = new Array();



  @Input()
  assetId: string;
  @Input()
  assetName: string;
  @Input()
  active: boolean;

  bsModalRef: BsModalRef;
  modalSub: ISubscription;

  constructor(
    private workOrderService: WorkerorderService,
    private modalService: BsModalService,
  ) { }

  ngOnInit() {
    if (this.active) {
      this.refresh();
    }
  }

  refresh() {
    this.workOrderService.GetWorkOrdersByAsset(this.assetId).subscribe(c => {
      this.workOrders = c.Result;
    });
  }

  createWorker() {
    const initialState = { assetId : this.assetId, assetName : this.assetName };
    this.bsModalRef = this.modalService.show(CreateWorkorderComponent, { initialState });
  }

}
