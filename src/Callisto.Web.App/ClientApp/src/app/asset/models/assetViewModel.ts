import { LocationViewModel } from "../../location/models/locationViewModel";

export class AssetViewModel {
  public Id: string;
  public AssetNumber: string;
  public Name: string;
  public Description: string;
}

export class AssetTreeViewModel extends AssetViewModel {
  public Children: number;
}

export class AssetDetailViewModel extends AssetViewModel {
  public PictureUrl: string;

  public Location: LocationViewModel;
}
