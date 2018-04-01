import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReportRoutes } from './routes';
import { RouterModule } from '@angular/router';
import { ReportInteractiveComponent } from './report-interactive/report-interactive.component';
import { ReportQnaComponent } from './report-qna/report-qna.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(ReportRoutes)
  ],
  declarations: [ReportInteractiveComponent, ReportQnaComponent]
})
export class ReportModule { }
