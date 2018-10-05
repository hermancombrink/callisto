import { Component, OnInit } from '@angular/core';
import { AuthService } from './core/auth.service';
import { AlertMessage, MessageSeverity, AlertDialog, AlertService } from './core/alert.service';
import { BsModalService } from 'ngx-bootstrap';
import { AlertDialogComponent } from './core/alert-dialog/alert-dialog.component';
import { UserViewModel, UserType } from './core/models/userViewModel';
import { RequestStatus } from './core/models/requestStatus';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import notify from 'devextreme/ui/notify';
import { detectBody } from './app.helpers';


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

  constructor(
    private alertService: AlertService,
    private authService: AuthService,
    private router: Router,
    private modalService: BsModalService) { }

  onResize() {
    detectBody();
  }

  ngOnInit() {
    detectBody();
    this.alertService.getDialogEvent().subscribe(alert => this.showDialog(alert));
    this.alertService.getMessageEvent().subscribe(message => this.showToast(message, false));
    this.alertService.getStickyMessageEvent().subscribe(message => this.showToast(message, true));

    this.loadUserProfile();

    this.authService.loggedIn.subscribe(c => {
      if (c.Status === RequestStatus.Success) {
        this.loadUserProfile();
        this.authService.currentUser.subscribe(x => this.user = x);
      }
    });

    this.authService.loggedOut.subscribe(c => {
      this.profileLoaded = false;
    });
  }


  loadUserProfile() {

    this.authService.GetUser().subscribe(c => {

      this.completedLoad = true;

      if (c.Status === RequestStatus.Success) {
        this.user = c.Result;
        this.profileLoaded = true;
        if (!this.user.ProfileCompleted) {
          switch (this.user.UserType) {
            default:
            case UserType.Member: {
              this.router.navigate(['/account/profile']);
              break;
            }
            case UserType.Vendor: {
              this.alertService.showWarningMessage('Vendor Not Implemented');
              break;
            }
            case UserType.Customer: {
              this.alertService.showWarningMessage('Customer Not Implemented');
              break;
            }
          }
        }
      } else {
        this.alertService.showWarningMessage(c.FriendlyMessage);
        this.signOut();
      }
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
    if (this.router.url.indexOf('/account/') < 0) {
      this.router.navigate(['/account/login']);
    }
  }

  showToast(message: AlertMessage, isSticky: boolean) {

    if (message == null) {
      return;
    }

    let type = 'success';
    switch (message.severity) {
      case MessageSeverity.success: type = 'success'; break;
      case MessageSeverity.error: type = 'error'; break;
      case MessageSeverity.warn: type = 'warning'; break;
      default: type = 'success'; break;
    }

    notify(message.detail || message.summary, type);
  }

  showDialog(dialog: AlertDialog) {

    const initialState = {
      dialog: dialog
    };
    this.modalService.show(AlertDialogComponent, { initialState });
  }
}
