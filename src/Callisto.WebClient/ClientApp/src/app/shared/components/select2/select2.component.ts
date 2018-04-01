import { Component, OnInit, Input, ElementRef } from '@angular/core';
import { LookupModel } from '../../models/lookupModel';

declare const $: any;

@Component({
  selector: 'app-select2',
  templateUrl: './select2.component.html',
  styleUrls: ['./select2.component.css']
})
export class Select2Component implements OnInit {

  @Input() LookupData: LookupModel[];
  @Input() PlaceHolder: string;
  @Input() AllowClear: true;

  constructor(private element: ElementRef) { }

  ngOnInit() {
    $(this.element.nativeElement).select2({
      placeholder: this.PlaceHolder,
      allowClear: true,
      width: '100%'
    });
  }

}
