
import { Component, OnInit, Input } from '@angular/core';
import { MapsAPILoader, AgmMap } from '@agm/core';
import { } from 'googlemaps';
import { ViewChild, NgZone, } from '@angular/core';
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

  @Input() searchInput: HTMLInputElement;

  @ViewChild('map') map: AgmMap;

  constructor(private mapsAPILoader: MapsAPILoader, private ngZone: NgZone) { }

  ngOnInit() {
    this.setCurrentPosition();
  }

  initComponent(locModel: LocationViewModel) {
    this.initAutoComplete();
    this.initLocation(locModel);
   }

  initAutoComplete() {
    this.mapsAPILoader.load().then(() => {
      this.setAutoComplete();
    }).catch(e => console.log(e));
  }

  initLocation(locModel: LocationViewModel) {
    if (!locModel || !locModel.Latitude || !locModel.Longitude) {
      this.showMarker = false;
      return;
    }
    this.model = locModel;
    this.showMarker = true;
    this.zoom = 12;
    this.draw();
  }

  draw() {
    this.map.triggerResize(true);
  }

  private setAutoComplete() {
    let autocomplete = new google.maps.places.Autocomplete(this.searchInput, {
      types: ['address']
    });
    autocomplete.addListener('place_changed', () => {
      this.ngZone.run(() => {

        let place: google.maps.places.PlaceResult = autocomplete.getPlace();

        if (place.geometry === undefined || place.geometry === null) {
          return;
        }

        this.zoom = 12;
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

        this.model.GoogleURL = place.url;
        this.model.GooglePlaceId = place.place_id;
        this.model.UTCOffsetMinutes = place.utc_offset;

        this.showMarker = true;
      });
    });
  }

  private findAddressItem(place: google.maps.places.PlaceResult, prop: string, useShort: boolean = false): string {
    try {
      let part = place.address_components.find(c => c.types.some(x => x === prop));
      if (part) {
        return useShort ? part.short_name : part.long_name;
      }
      return '';
    } catch {
      console.error('failed to find ' + prop);
      return '';
    }
  }

  private setCurrentPosition() {
    if (!this.showMarker) {
      if ('geolocation' in navigator) {
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
