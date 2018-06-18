export class AddCustomerViewModel {
    Email: string;
    FirstName: string;
    LastName: string;
    CreateAccount = true;
    SendLink = true;
}

export class CustomerViewModel {
    Email: string;
    FirstName: string;
    LastName: string;
    ParentId?: string;
    PictureUrl: string;
}
