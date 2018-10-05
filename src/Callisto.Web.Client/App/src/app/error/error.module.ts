import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Err404Component } from './err404/err404.component';
import { Err500Component } from './err500/err500.component';
import { RouterModule } from '@angular/router';
import { ErrorRoutes } from './routes';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(ErrorRoutes)
  ],
  declarations: [Err404Component, Err500Component],
  exports: [RouterModule]
})
export class ErrorModule { }
