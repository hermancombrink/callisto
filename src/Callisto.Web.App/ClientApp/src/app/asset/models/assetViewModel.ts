export class AssetViewModel {
  public Id: string;
  public AssetNumber: string;
  public Name: string;
  public Description: string;
}


export class AssetTreeViewModel extends AssetViewModel {
  public Children: number;
}
