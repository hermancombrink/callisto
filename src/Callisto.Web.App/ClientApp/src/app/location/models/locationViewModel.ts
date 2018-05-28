export class LocationViewModel  {
  public Id?: string;
  public Latitude: number;
  public Longitude: number;

  public FormattedAddress: string;

  public Route?: string;
  public Vicinity?: string;
  public City?: string;
  public State?: string;
  public Country?: string;
  public PostCode?: string;

  public CountryCode?: string;
  public StateCode?: string;

  public UTCOffsetMinutes?: number;
  public GooglePlaceId?: string;
  public GoogleURL?: string;
}
