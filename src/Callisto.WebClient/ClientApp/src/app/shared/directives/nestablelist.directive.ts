import { Directive, ElementRef, OnInit, AfterViewInit } from '@angular/core';

declare var $: any;

@Directive({
  // tslint:disable-next-line:directive-selector
  selector: '[nestablelist]'
})
export class NestablelistDirective implements OnInit, AfterViewInit {

  ngAfterViewInit(): void {
    $(this.element.nativeElement).nestable({ group: 1 });
  }
  ngOnInit(): void {
  }
  constructor(private element: ElementRef) { }

}
