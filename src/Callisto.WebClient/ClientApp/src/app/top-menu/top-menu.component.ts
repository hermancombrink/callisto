import { Component, OnInit } from '@angular/core';
import { smoothlyMenu } from '../app.helpers';
import {Location} from '@angular/common';
import { AuthserviceService } from '../../services/authservice.service';
import { AlertService, MessageSeverity } from '../../services/alert.service';

declare var jQuery: any;

@Component({
  selector: 'app-top-menu',
  templateUrl: './top-menu.component.html',
  styleUrls: ['./top-menu.component.css']
})
export class TopMenuComponent implements OnInit {

  constructor(private authService : AuthserviceService, private alertService : AlertService, private location: Location) { }

  ngOnInit() {
  }


  toggleNavigation(): void {
    jQuery("body").toggleClass("mini-navbar");
    smoothlyMenu();
  }

  Logout() {
    this.authService.Logout().subscribe(data => {
        console.log("awesome");
    }, error => {
      if(error.status === 200)
      {
        window.location.href = error.url; 
      }
      else
      {
        this.alertService.showMessage("Error", "Oops something went wrong", MessageSeverity.error);
      }
    });
  }

}
