import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';
import { MessageSeverity, AlertDialog, DialogType } from '../alert.service';

@Component({
  selector: 'app-alert-dialog',
  templateUrl: './alert-dialog.component.html',
  styleUrls: ['./alert-dialog.component.css']
})
export class AlertDialogComponent implements OnInit {

  dialog: AlertDialog;

  cssIcon: string;

  constructor(public bsModalRef: BsModalRef) { }

  ngOnInit() {

    switch (this.dialog.severity) {

      case MessageSeverity.error: {
        this.cssIcon = 'fa fa-stop-circle';
        break;
      }
      case MessageSeverity.success: {
        this.cssIcon = 'fa fa-check-circle';
        break;
      }
      case MessageSeverity.wait: {
        this.cssIcon = 'fa fa-stopwatch';
        break;
      }
      case MessageSeverity.warn: {
        this.cssIcon = 'fa fa-exclamation-triangle';
        break;
      }
      case MessageSeverity.info:
      default: {
        this.cssIcon = 'fa fa-info-circle';
        break;
      }
    }
  }

  onCancel() {
    if (this.dialog.cancelCallback) {
      this.dialog.cancelCallback();
    }
    this.bsModalRef.hide();
  }

  onSubmit() {
    if (this.dialog.okCallback) {
      this.dialog.okCallback();
    };
    this.bsModalRef.hide();
  }

}
