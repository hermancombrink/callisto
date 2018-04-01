import { Component, OnInit, ElementRef, Input } from '@angular/core';

declare var $: any;

@Component({
  selector: 'app-icheck',
  templateUrl: './icheck.component.html',
  styleUrls: ['./icheck.component.css']
})
export class IcheckComponent implements OnInit {

  @Input() Key: string;
  @Input() Value: string;

  constructor(private element: ElementRef) { }

  ngOnInit() {
    $(this.element.nativeElement).iCheck({
      checkboxClass: 'icheckbox_square-green',
      radioClass: 'iradio_square-green',
    });
  }
}
