import { Component, AfterViewInit, OnInit, ElementRef, Input } from '@angular/core';
import { Router } from '@angular/router';
import 'jquery-slimscroll';
import { AuthService } from '../../core/auth.service';
import { RequestStatus } from '../../core/models/requestStatus';
import { UserViewModel } from '../../core/models/userViewModel';
import { AlertService, MessageSeverity } from '../../core/alert.service';

declare var $: any;

@Component({
  selector: 'app-left-menu',
  templateUrl: './left-menu.component.html',
  styleUrls: ['./left-menu.component.css']
})
export class LeftMenuComponent implements AfterViewInit, OnInit {

  @Input() user: UserViewModel = new UserViewModel();
  menuElement: any;

  constructor(private router: Router,
    private authService: AuthService,
    private element: ElementRef,
    private alertService: AlertService) {

  }

  ngAfterViewInit() {
    this.menuElement = this.element.nativeElement.querySelector('#side-menu');
    (<any>$(this.menuElement)).metisMenu();

    if ($('body').hasClass('fixed-sidebar')) {
      $('.sidebar-collapse').slimscroll({
        height: '100%'
      });
    }
  }

  ngOnInit(): void {
  }

  Logout() {
    this.authService.SignOut().subscribe(c => {
      this.alertService.showMessage('Thanks for visiting', '', MessageSeverity.info);
      this.router.navigate(['/account/login']);
    });
  }

  activeRoute(routename: string): boolean {
    return this.router.url === (routename);
  }

}
