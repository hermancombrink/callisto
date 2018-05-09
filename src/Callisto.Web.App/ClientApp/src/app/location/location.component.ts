import { Component, OnInit, Input } from '@angular/core';
import { MapsAPILoader } from '@agm/core';
import { } from '@types/googlemaps';
import { ViewChild, ElementRef, NgZone, } from '@angular/core';
import { LocationViewModel } from './models/locationViewModel';

@Component({
  selector: 'app-location',
  templateUrl: './location.component.html',
  styleUrls: ['./location.component.css']
})
export class LocationComponent implements OnInit {

  public zoom: number;

  @Input() model: LocationViewModel = new LocationViewModel();

  @Input() showMarker = false;

  @Input() searchInput: HTMLInputElement


  constructor(private mapsAPILoader: MapsAPILoader, private ngZone: NgZone) { }

  ngOnInit() {

    this.setCurrentPosition();

    this.mapsAPILoader.load().then(() => {
      let autocomplete = new google.maps.places.Autocomplete(this.searchInput, {
        types: ["address"]
      });
      autocomplete.addListener("place_changed", () => {
        this.ngZone.run(() => {
          //get the place result
          let place: google.maps.places.PlaceResult = autocomplete.getPlace();

          //verify result
          if (place.geometry === undefined || place.geometry === null) {
            return;
          }

          this.zoom = 12;

          //set latitude, longitude and zoom
          this.model.Latitude = place.geometry.location.lat();
          this.model.Longitude = place.geometry.location.lng();

          this.model.FormatterAddress = place.formatted_address;

          this.model.Route = place.address_components.find(c => c.types.some(x => x == 'route')).long_name;
          this.model.Vicinity = place.address_components.find(c => c.types.some(x => x == 'sublocality')).long_name;
          this.model.City = place.address_components.find(c => c.types.some(x => x == 'locality')).long_name;
          this.model.State = place.address_components.find(c => c.types.some(x => x == 'administrative_area_level_1')).long_name;
          this.model.Country = place.address_components.find(c => c.types.some(x => x == 'country')).long_name;

          this.model.PostCode = place.address_components.find(c => c.types.some(x => x == 'postal_code')).long_name;

          this.model.StateCode = place.address_components.find(c => c.types.some(x => x == 'administrative_area_level_1')).short_name;
          this.model.CountryCode = place.address_components.find(c => c.types.some(x => x == 'country')).short_name;

          this.model.GoogleUrl = place.url;
          this.model.GooglePlaceId = place.place_id;
          this.model.UTCOffsetMinutes = place.utc_offset;

          this.showMarker = true;
        });
      });
    });
  }

  private setCurrentPosition() {
    if (!this.showMarker) {
      if ("geolocation" in navigator) {
        navigator.geolocation.getCurrentPosition((position) => {
          this.model.Latitude = position.coords.latitude;
          this.model.Longitude = position.coords.longitude;
        });
      }
      this.zoom = 8;
    } else {
      this.zoom = 12;
    }
    
  }

}
