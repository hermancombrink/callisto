import { Directive, ElementRef, OnInit, Input, style } from '@angular/core';
import * as pbi from 'powerbi-client/dist/powerbi.min';
import { StylesCompileDependency } from '@angular/compiler';
import { PowersampleService } from '../services/powersample.service';

@Directive({
  // tslint:disable-next-line:directive-selector
  selector: 'powerbi-reportqa'
})
export class PwrbireportqaDirective implements OnInit {

  @Input() question = 'This year sales by store type by postal code as map';

  config = {
    type: 'qna',
    tokenType: 1,
    accessToken: '',
    embedUrl: '',
    datasetIds: [],
    viewMode: 0,
    question: this.question
  };
  ngOnInit(): void {
    this.powerBiService.getQAAccessData().subscribe((response) => {
      const isMobile = window.matchMedia('screen and (max-width: 768px)').matches;
      const powerbi = new pbi.service.Service(pbi.factories.hpmFactory, pbi.factories.wpmpFactory, pbi.factories.routerFactory);
      this.config.accessToken = response.embedToken.token;
      this.config.embedUrl = `${response.embedUrl}?isMobile=${isMobile}`;
      this.config.datasetIds.push(response.id);
      powerbi.embed(this.element.nativeElement, this.config);
    }, (e) => {
      console.log(e);
    });
  }
  constructor(private element: ElementRef, private powerBiService: PowersampleService) { }

}
