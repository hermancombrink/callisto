import { Directive, ElementRef, OnInit, Input, style } from '@angular/core';
import * as pbi from 'powerbi-client/dist/powerbi.min';
import { StylesCompileDependency } from '@angular/compiler';
import { PowersampleService } from '../services/powersample.service';

@Directive({
  // tslint:disable-next-line:directive-selector
  selector: 'powerbi-dashboard'
})
export class PwrbidashboardDirective implements OnInit {
 @Input() reportToken: string;
 @Input() reportUrl: string;
 @Input() reportId: string;

 private config = {
    type: 'report',
    tokenType: 1,
    accessToken: '',
    embedUrl: '',
    id: '',
    permissions: 7,
    settings: {
        filterPaneEnabled: true,
        navContentPaneEnabled: true
    }
  };

  ngOnInit(): void {
    this.powerBiService.getReportAccessData().subscribe((response) => {
      const powerbi = new pbi.service.Service(pbi.factories.hpmFactory, pbi.factories.wpmpFactory, pbi.factories.routerFactory);
      this.config.accessToken = response.embedToken.token;
      this.config.embedUrl = response.embedUrl;
      this.config.id = response.id;
      powerbi.embed(this.element.nativeElement, this.config);
    }, (e) => {
      console.log(e);
    });
  }

  constructor(private element: ElementRef, private powerBiService: PowersampleService) { }
}
