import { Component, OnInit, Input } from '@angular/core';
import { MapsAPILoader, AgmMap } from '@agm/core';
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

  @ViewChild('map') map: AgmMap;


  private hasLoaded = false;

  constructor(private mapsAPILoader: MapsAPILoader, private ngZone: NgZone) { }

  ngOnInit() {
    this.setCurrentPosition();
    this.mapsAPILoader.load().then(() => {
      this.setAutoComplete();
    }).catch(e => console.log(e));
  }

  draw() {
    this.map.triggerResize(true);
  }

  setLocation(locModel: LocationViewModel) {
    if (!locModel) {
      return;
    }

    this.model = locModel;
    this.showMarker = true;
    this.zoom = 12;
  }

  private setAutoComplete() {
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

        this.model.FormattedAddress = place.formatted_address;

        this.model.Route = this.findAddressItem(place, 'route');
        this.model.Vicinity = this.findAddressItem(place, 'sublocality');
        this.model.City = this.findAddressItem(place, 'locality');
        this.model.State = this.findAddressItem(place, 'administrative_area_level_1');
        this.model.Country = this.findAddressItem(place, 'country');

        this.model.PostCode = this.findAddressItem(place, 'postal_code');

        this.model.StateCode = this.findAddressItem(place, 'administrative_area_level_1', true);
        this.model.CountryCode = this.findAddressItem(place, 'country', true);

        this.model.GoogleUrl = place.url;
        this.model.GooglePlaceId = place.place_id;
        this.model.UTCOffsetMinutes = place.utc_offset;

        this.showMarker = true;
      });
    });
  }

  private findAddressItem(place: google.maps.places.PlaceResult, prop: string, useShort: boolean = false): string {
    try {
      let part = place.address_components.find(c => c.types.some(x => x == prop));
      if (part) {
        return useShort ? part.short_name : part.long_name;
      }
      return '';
    }
    catch {
      console.error('failed to find ' + prop);
      return '';
    }
  }

  private setCurrentPosition() {
    if (!this.showMarker) {
      if ("geolocation" in navigator) {
        navigator.geolocation.getCurrentPosition((position) => {
          if (this.model.Latitude) {
            return;
          }
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
