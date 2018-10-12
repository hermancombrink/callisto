import { Component, OnInit, AfterViewInit, Input } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { environment } from '../../../environments/environment';
import { AuthService } from '../../core/auth.service';
import { AlertService } from '../../core/alert.service';
import { RequestStatus } from '../../core/models/requestStatus';
import { RequestResult } from '../../core/models/requestResult';

@Component({
  selector: 'app-upload-pic',
  templateUrl: './upload-pic.component.html',
  styleUrls: ['./upload-pic.component.css']
})
export class UploadPicComponent implements OnInit, AfterViewInit {

  uploader: FileUploader;
  hasBaseDropZoneOver = false;

  @Input()
  pictureUrl: string;

  @Input()
  entityType = "";

  @Input()
  entityId = "";

  constructor(private authService: AuthService,
    private alertService: AlertService
    ) { }

  ngOnInit() {
    this.initComponent();
  }

  ngAfterViewInit(): void {
  }

  fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  initComponent() {
    this.uploader = new FileUploader({
      url: `${environment.apiUrl}pic/${this.entityType}/${this.entityId}`,
      autoUpload: true,
      authToken: this.authService.authToken
    });
    this.uploader.onCompleteItem = (item: any, response: any, status: any, headers: any) => {
      let result = <RequestResult>JSON.parse(response)
      if (status === 200 && result.Status === RequestStatus.Success) {
        this.pictureUrl = result.Result;
      } else {
        this.alertService.showWarningMessage(result.FriendlyMessage);
      }
    };
  }

}
