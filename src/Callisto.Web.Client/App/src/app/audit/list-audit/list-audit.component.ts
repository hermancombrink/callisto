import { Component, OnInit, Input } from '@angular/core';
import { AuditViewModel } from '../model/auditViewModel';
import { AuditService } from '../audit.service';

@Component({
  selector: 'app-list-audit',
  templateUrl: './list-audit.component.html',
  styleUrls: ['./list-audit.component.css']
})
export class ListAuditComponent implements OnInit {

  model: AuditViewModel[] = [];

  @Input()
  entityType = "";

  @Input()
  entityId = "";

  constructor(
    private auditService: AuditService
  ) { }

  ngOnInit() {
  }

  initComponent() {
    this.auditService.GetAuditHistory(this.entityType, this.entityId).subscribe(c => this.model = c.Result);
  }

}
