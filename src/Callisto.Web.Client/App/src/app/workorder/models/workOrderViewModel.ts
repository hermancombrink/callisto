export class BaseWorkOrderViewModel {
    public AssetId: string;
    public Description: string
    public Instructions: string
    public SafetyNotes: string
    public DueDate: Date;
}


export class WorkOrderItemViewModel extends BaseWorkOrderViewModel {

    public Completed: string;
    public WorkOrderNumber: string;
    public RequestDate?: Date;
    public ApprovalDate?: Date;
}

