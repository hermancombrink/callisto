import { Component, AfterViewInit } from '@angular/core';
import { Router } from '@angular/router';
import 'jquery-slimscroll';

declare var $: any;

@Component({
  selector: 'app-left-menu',
  templateUrl: './left-menu.component.html',
  styleUrls: ['./left-menu.component.css']
})
export class LeftMenuComponent implements AfterViewInit {

  constructor(private router: Router) { }

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
