export class AddStaffViewModel {
    Email: string;
    FirstName: string;
    LastName: string;
    CreateAccount = true;
    SendLink = true;
}


export class StaffViewModel {
    Email: string;
    FirstName: string;
    LastName: string;
    ParentId?: string;
    PictureUrl: string;
}
