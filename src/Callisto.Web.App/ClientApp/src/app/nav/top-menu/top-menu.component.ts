import { Component, OnInit } from '@angular/core';
import { smoothlyMenu } from '../../app.helpers';
import { Router } from '@angular/router';
import { AuthService } from '../../core/auth.service';
import { MessageSeverity, AlertService, DialogType } from '../../core/alert.service';

declare var $: any;

@Component({
  selector: 'app-top-menu',
  templateUrl: './top-menu.component.html',
  styleUrls: ['./top-menu.component.css']
})
export class TopMenuComponent implements OnInit {

  constructor(private router: Router,
    private authService: AuthService,
    private alertService: AlertService) { }

  ngOnInit() {
 
  }

  toggleNavigation(): void {
    $('body').toggleClass('mini-navbar');
    smoothlyMenu();
  }

  Logout() {
    this.authService.SignOut().subscribe(c => {
      this.alertService.showMessage('Thanks for visiting', '', MessageSeverity.info);
      this.router.navigate(['/account/login']);
    });

  }

}
