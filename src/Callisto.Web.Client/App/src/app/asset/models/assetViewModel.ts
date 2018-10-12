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

  public ModelNumber: string;
  public SerialNumber: string;
  public BarcodeNumber: string;
  public FinanceAssetNumber: string;

  public ReadingType: number;
  public StatusType: number;
  public CriticalType: number;

  public ParentId: string;

  public IsActive: boolean;

  public ContractorId: number;
  public CustomerId: number;
  public DepartmentId: number;
  public ManufacturerId: number;

  public DimensionData: AssetDimensionViewModel = new AssetDimensionViewModel();

  public FinanceData: AssetFinanceViewModel = new AssetFinanceViewModel();

  public LocationData: LocationViewModel = new LocationViewModel();

  public Tags: string[];
}

export class AssetFinanceViewModel  {

 public PurchaseDate: Date;

 public PurchaseCost: number;
 public PurchaseOrder: string;
 public Supplier: string;

 public ExpectedLifeYears: number;
 public ExpectedLifeHours: number;
 public SalvageValue: number;
 public DepreciationValue: number;
 public CurrentValue: number;

 public DepreciationType: number;

 public MarketValue: number;
 public ReplacementValue: number;
 public ChangeOverValue: number;

 public ReceivedDate: Date;
 public LastEvaluationDate: Date;
 public NextEvaluationDate: Date;
}

export class AssetInspectionViewModel {
public CertificateNumber: string;
public CompletedBy: string;
public Comment: string;
public LastInspectionDate: Date;
public NextInspectionDate: Date;
}

export class AssetDimensionViewModel {
  public Height: number;
  public Width: number;
  public Depth: number;
  public Weight: number;
}
