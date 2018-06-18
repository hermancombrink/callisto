export class AddVendorViewModel {
    Email: string;
    FirstName: string;
    LastName: string;
    CreateAccount = true;
    SendLink = true;
}


export class VendorViewModel {
    Email: string;
    FirstName: string;
    LastName: string;
    ParentId?: string;
    PictureUrl: string;
}
