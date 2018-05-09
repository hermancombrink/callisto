import { Component, OnInit, Input } from '@angular/core';
import { MapsAPILoader } from '@agm/core';
import { } from '@types/googlemaps';
import { ViewChild, ElementRef, NgZone, } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-location',
  templateUrl: './location.component.html',
  styleUrls: ['./location.component.css']
})
export class LocationComponent implements OnInit {

  public latitude: number;
  public longitude: number;
  public fillInAddress: '';
  public searchControl: FormControl;
  public zoom: number;
  public f_addr: string;
  public city: string;
  public country: string;
  public scountry: string;
  public postCode: string;
  public estab: string;
  public state: string;

  public utcoffset: number;
  public placeId: string;
  public url: string;
  public route: string;
  public vicinity: string;

  @ViewChild('search') public searchElement: ElementRef;

  constructor(private mapsAPILoader: MapsAPILoader, private ngZone: NgZone) { }

  ngOnInit() {
    this.setCurrentPosition();

    this.mapsAPILoader.load().then(() => {
      let autocomplete = new google.maps.places.Autocomplete(this.searchElement.nativeElement, {
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

          //set latitude, longitude and zoom
          this.latitude = place.geometry.location.lat();
          this.longitude = place.geometry.location.lng();
          this.zoom = 12;
          this.f_addr = place.name;
          this.country = place.address_components.find(c => c.types.some(x => x == 'country')).long_name;
          this.scountry = place.address_components.find(c => c.types.some(x => x == 'country')).short_name;
          this.postCode = place.address_components.find(c => c.types.some(x => x == 'postal_code')).long_name;
          this.state = place.address_components.find(c => c.types.some(x => x == 'administrative_area_level_1')).long_name;
          this.estab = place.address_components.find(c => c.types.some(x => x == 'administrative_area_level_1')).short_name;
          this.city = place.address_components.find(c => c.types.some(x => x == 'locality')).long_name;
          console.log(place);
          //c => c.types.find(x => x == 'country')).long_name;
        });
      });
    });
  }

  private setCurrentPosition() {
    if ("geolocation" in navigator) {
      navigator.geolocation.getCurrentPosition((position) => {
        this.latitude = position.coords.latitude;
        this.longitude = position.coords.longitude;
        this.zoom = 12;
      });

    }
  }

}
