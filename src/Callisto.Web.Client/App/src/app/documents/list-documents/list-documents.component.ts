import { Component, OnInit, Input, ViewChild, AfterViewInit } from '@angular/core';
import { environment } from '../../../environments/environment.prod';
import { DxFileUploaderComponent } from 'devextreme-angular';

@Component({
  selector: 'app-list-documents',
  templateUrl: './list-documents.component.html',
  styleUrls: ['./list-documents.component.css']
})
export class ListDocumentsComponent implements OnInit, AfterViewInit {

  @ViewChild('docUploader') docUploader: DxFileUploaderComponent;

  @Input()
  entityType = "";

  @Input()
  entityId = "";

  apiUrl = environment.apiUrl;

  constructor() { }

  ngOnInit() {
    this.docUploader.uploadUrl = `${this.apiUrl}doc/${this.entityType}/${this.entityId}`
  }

  ngAfterViewInit(): void {
  }

}
