import { Component, OnInit } from '@angular/core';
import { smoothlyMenu } from '../../app.helpers';

declare var $: any;

@Component({
  selector: 'app-top-menu',
  templateUrl: './top-menu.component.html',
  styleUrls: ['./top-menu.component.css']
})
export class TopMenuComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  toggleNavigation(): void {
    $('body').toggleClass('mini-navbar');
    smoothlyMenu();
  }

}
