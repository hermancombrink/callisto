import { Component, AfterViewInit, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import 'jquery-slimscroll';
import { AuthService } from '../../core/auth.service';
import { RequestStatus } from '../../core/models/requestStatus';
import { UserViewModel } from '../../core/models/userViewModel';

declare var $: any;

@Component({
  selector: 'app-left-menu',
  templateUrl: './left-menu.component.html',
  styleUrls: ['./left-menu.component.css']
})
export class LeftMenuComponent implements AfterViewInit, OnInit {

  user: UserViewModel;

  constructor(private router: Router, private authService: AuthService) { }

  ngAfterViewInit() {
    $('#side-menu').metisMenu();

    if ($('body').hasClass('fixed-sidebar')) {
      $('.sidebar-collapse').slimscroll({
        height: '100%'
      });
    }
  }

  ngOnInit(): void {
    this.authService.GetUser().subscribe(c => {
      if (c.status == RequestStatus.Success) {
        this.user = c.result;
      }
    })

    
  }

  activeRoute(routename: string): boolean {
    return this.router.url === (routename);
  }

}
