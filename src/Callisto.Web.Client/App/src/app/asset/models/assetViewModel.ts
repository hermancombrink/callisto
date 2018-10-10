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

  public ReadingId: number;
  public StatusId: number;
  public CriticalId: number;

  public ContractorId: number;
  public CustomerId: number;
  public DepartmentId: number;
  public ManufacturerId: number;
}

export class AssetFinanceViewModel  {
 public PurchaseDate: Date;
 public OrderCost: number;
 public PurchaseOrder: string;
 public Supplier: string;
 public ReceivedDate: Date;
 public ExpectedLifeYears: number;
 public ExpectedLifeHours: number;
 public SalvageValue: number;
 public DepreciationValue: number;
 public CurrentValue: number;
 public DepreciationType: number;
 public MarketValue: number;
 public ReplacementValue: number;
 public ChangeOverValue: number;
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
