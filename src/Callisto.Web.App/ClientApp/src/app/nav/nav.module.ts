import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LeftMenuComponent } from './left-menu/left-menu.component';
import { TopMenuComponent } from './top-menu/top-menu.component';
import { RightMenuComponent } from './right-menu/right-menu.component';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [
    CommonModule,
    RouterModule
  ],
  declarations: [LeftMenuComponent, TopMenuComponent, RightMenuComponent],
  exports: [LeftMenuComponent, TopMenuComponent, RightMenuComponent]
})
export class NavModule { }
