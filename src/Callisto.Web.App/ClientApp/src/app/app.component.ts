import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthService } from './core/auth.service';
import { Subscription } from 'rxjs/Subscription';
import { AlertMessage, DialogType, MessageSeverity, AlertDialog, AlertService } from './core/alert.service';
import { ToastOptions, ToastData, ToastyService, ToastyConfig } from 'ng2-toasty';
import { BsModalService } from 'ngx-bootstrap';
import { AlertDialogComponent } from './core/alert-dialog/alert-dialog.component';
import { UserViewModel } from './core/models/userViewModel';
import { RequestStatus } from './core/models/requestStatus';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  stickyToasties: number[] = [];

  user: UserViewModel = new UserViewModel();
  profileLoaded = false;
  completedLoad = false;

  constructor(private toastyService: ToastyService,
    private toastyConfig: ToastyConfig,
    private alertService: AlertService,
    private authService: AuthService,
    private router: Router,
    private modalService: BsModalService) {

    this.setToastSettings();
  }

  ngOnInit() {
    this.alertService.getDialogEvent().subscribe(alert => this.showDialog(alert));
    this.alertService.getMessageEvent().subscribe(message => this.showToast(message, false));
    this.alertService.getStickyMessageEvent().subscribe(message => this.showToast(message, true));

    this.loadUserProfile();

    this.authService.loggedIn.subscribe(c => {
      if (c.Status == RequestStatus.Success) {
        this.loadUserProfile();
      }
    });

    this.authService.loggedOut.subscribe(c => {
      this.profileLoaded = false;
    });
  }

  loadUserProfile() {

    this.authService.GetUser().subscribe(c => {
      if (c.Status == RequestStatus.Success) {
        this.user = c.Result;
        this.profileLoaded = true;
      } else {
        this.alertService.showWarningMessage(c.FriendlyMessage);
        this.signOut();
      }
      this.completedLoad = true;
    }, err => {
      if (err instanceof HttpErrorResponse) {
        switch (err.status) {
          case 401: {
            this.signOut();
            break;
          }
          case 404: {
            this.router.navigate(['/error/404']);
            break;
          }
          default:
          case 500:
          case 0: {
            this.alertService.showErrorMessage();
            this.router.navigate(['/error/500']);
            break;
          }
        }
      }

      this.completedLoad = true;
    });
  }

  signOut() {
    this.authService.ClearToken();
    this.router.navigate(['/account/login']);
  }

  setToastSettings() {
    this.toastyConfig.theme = 'bootstrap';
    this.toastyConfig.position = 'top-right';
    this.toastyConfig.limit = 100;
    this.toastyConfig.showClose = true;
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

  showDialog(dialog: AlertDialog) {

    const initialState = {
      dialog: dialog
    };
    this.modalService.show(AlertDialogComponent, { initialState });
  }
}
