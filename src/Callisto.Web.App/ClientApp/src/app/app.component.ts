import { Component, OnDestroy } from '@angular/core';
import { AuthService } from './core/auth.service';
import { Subscription } from 'rxjs/Subscription';
import { AlertMessage, DialogType, MessageSeverity, AlertDialog, AlertService } from './core/alert.service';
import { ToastOptions, ToastData, ToastyService, ToastyConfig } from 'ng2-toasty';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent  {

  stickyToasties: number[] = [];

  constructor(private toastyService: ToastyService, private toastyConfig: ToastyConfig,
    private alertService: AlertService) {

    this.toastyConfig.theme = 'bootstrap';
    this.toastyConfig.position = 'top-right';
    this.toastyConfig.limit = 100;
    this.toastyConfig.showClose = true;
  }

  ngOnInit() {
    this.alertService.getMessageEvent().subscribe(message => this.showToast(message, false));
    this.alertService.getStickyMessageEvent().subscribe(message => this.showToast(message, true));
  }

  showToast(message: AlertMessage, isSticky: boolean) {

    if (message == null) {
      for (const id of this.stickyToasties.slice(0)) {
        this.toastyService.clear(id);
      }

      return;
    }

    const toastOptions: ToastOptions = {
      title: message.summary,
      msg: message.detail,
      timeout: isSticky ? 0 : 4000
    };


    if (isSticky) {
      toastOptions.onAdd = (toast: ToastData) => this.stickyToasties.push(toast.id);

      toastOptions.onRemove = (toast: ToastData) => {
        const index = this.stickyToasties.indexOf(toast.id, 0);

        if (index > -1) {
          this.stickyToasties.splice(index, 1);
        }

        toast.onAdd = null;
        toast.onRemove = null;
      };
    }


    switch (message.severity) {
      case MessageSeverity.default: this.toastyService.default(toastOptions); break;
      case MessageSeverity.info: this.toastyService.info(toastOptions); break;
      case MessageSeverity.success: this.toastyService.success(toastOptions); break;
      case MessageSeverity.error: this.toastyService.error(toastOptions); break;
      case MessageSeverity.warn: this.toastyService.warning(toastOptions); break;
      case MessageSeverity.wait: this.toastyService.wait(toastOptions); break;
    }
  }

}
