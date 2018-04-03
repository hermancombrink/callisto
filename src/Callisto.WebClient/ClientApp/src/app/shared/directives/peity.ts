import { OnInit, OnChanges, ElementRef, Input, NgModule, Directive, AfterViewInit } from '@angular/core';

// Import Peity chart library
import 'peity';

declare var $: any;

@Directive({
  // tslint:disable-next-line:directive-selector
  selector: 'span[peity]',
  exportAs: 'peity-chart',

})
export class PeityDirective implements OnChanges, OnInit, AfterViewInit {

  // Properties
  @Input() public options: any;
  @Input() public type: any;

  public chart: any;
  private element: ElementRef;
  private initFlag = false;

  public constructor(element: ElementRef) {
    this.element = element;
  }

  // Initialise
  public ngOnInit(): any {
    this.initFlag = true;
    if (this.options || this.type) {
      this.build();
    }
  }

  public ngAfterViewInit(): void {
    if (this.initFlag) {
      this.build();
    }
  }

  // Build
  private build(): any {

    // Check if peity is available
    if (typeof $(this.element).peity === 'undefined') {
      throw new Error('Configuration issue: Embedding peity lib is mandatory');
    }

    // Let's build chart
    this.chart = $(this.element.nativeElement).peity(this.type, this.options);
  }

  // Change
  public ngOnChanges(): void {
    if (this.initFlag) {
      this.build();
    }
  }

}
