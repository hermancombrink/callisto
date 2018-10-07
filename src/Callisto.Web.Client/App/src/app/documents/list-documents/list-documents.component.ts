import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-list-documents',
  templateUrl: './list-documents.component.html',
  styleUrls: ['./list-documents.component.css']
})
export class ListDocumentsComponent implements OnInit {

  @Input()
  entityType = "";

  @Input()
  entityId = "";

  constructor() { }

  ngOnInit() {
  }

}
