import { Component, OnInit, OnDestroy } from '@angular/core';

declare var $: any;

@Component({
  template: ''
})
export class BaseComponent implements OnInit, OnDestroy {

  constructor() { }

  ngOnInit() {
    $("body").addClass("white-bg");
  }

  ngOnDestroy(): void {
    $("body").removeClass("white-bg");
  }

}
