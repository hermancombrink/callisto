import { Component, OnInit } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { AlertService } from '../../core/alert.service';
import { Router } from '@angular/router';
import { CreateModalComponent } from '../create-modal/create-modal.component';

@Component({
  selector: 'app-view',
  templateUrl: './view.component.html',
  styleUrls: ['./view.component.css']
})
export class ViewComponent implements OnInit {

  bsModalRef: BsModalRef;

  constructor(private modalService: BsModalService,
    private alertService: AlertService,
    private router: Router) {
    this.modalService.onHidden.subscribe(c => this.loadMembers());
  }

  ngOnInit() {
    this.loadMembers();
  }

  createMember() {
    this.bsModalRef = this.modalService.show(CreateModalComponent);
  }

  loadMembers() {

  }
}
