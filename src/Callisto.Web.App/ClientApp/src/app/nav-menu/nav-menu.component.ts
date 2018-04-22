import { Component, AfterViewInit } from '@angular/core';
import { Router } from '@angular/router';

declare var $: any;

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements AfterViewInit {
  constructor(private router: Router) { }
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  ngAfterViewInit() {
    $('#side-menu').metisMenu();

    if ($('body').hasClass('fixed-sidebar')) {
      $('.sidebar-collapse').slimscroll({
        height: '100%'
      });
    }
  }

  activeRoute(routename: string): boolean {
    return this.router.url === (routename);
  }
}
