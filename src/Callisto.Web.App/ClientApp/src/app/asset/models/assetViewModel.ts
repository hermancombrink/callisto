import { LocationViewModel } from '../../location/models/locationViewModel';

export class AssetViewModel {
  public Id: string;
  public AssetNumber: string;
  public Name: string;
  public Description: string;
}

export class AssetInfoViewModel extends AssetViewModel {

  public HasLocation: boolean;
  public Latitude: number;
  public Longitude: number;
  public FormattedAddress: string;
  public PictureUrl: string;
}

export class AssetTreeViewModel extends AssetViewModel {
  public Children: number;
  public ParentId: string;
}

export class AssetDetailViewModel extends AssetViewModel {
  public PictureUrl: string;
  public Location: LocationViewModel;
  public ParentId: string;
}
