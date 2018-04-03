import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Select2Component } from './components/select2/select2.component';
import { PeityDirective } from './directives/peity';
import { IcheckComponent } from './components/icheck/icheck.component';
import { NestablelistDirective } from './directives/nestablelist.directive';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [Select2Component, PeityDirective, IcheckComponent, NestablelistDirective],
  exports: [Select2Component, PeityDirective, IcheckComponent, NestablelistDirective]
})
export class SharedModule { }
