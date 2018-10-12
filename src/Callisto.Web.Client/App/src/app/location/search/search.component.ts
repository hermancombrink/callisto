import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { LocationViewModel } from '../models/locationViewModel';
import { LocationComponent } from '../location.component';

@Component({
  selector: 'app-search-location',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  mapVisible = false;

  @ViewChild('locationMap') locationMap: LocationComponent;
  @ViewChild('searchInput') searchInput: ElementRef;

  constructor() { }

  ngOnInit() {
  }

  showMap() {
    this.mapVisible = !this.mapVisible;
    if (this.mapVisible) {
      this.locationMap.draw();
    }
  }

  initComponent(locModel: LocationViewModel) {
    this.searchInput.nativeElement.value =  locModel.FormattedAddress ? locModel.FormattedAddress : "";
    this.locationMap.initAutoComplete();
    this.locationMap.initLocation(locModel);
   }

   getModel() {
     let model = this.locationMap.model;
     model.FormattedAddress = this.searchInput.nativeElement.value
     return model;
   }

}
