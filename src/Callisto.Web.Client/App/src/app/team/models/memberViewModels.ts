export class AddMemberViewModel {
    Email: string;
    FirstName: string;
    LastName: string;
    CreateAccount = true;
    SendLink = true;
}


export class MemberViewModel {
    Email: string;
    FirstName: string;
    LastName: string;
    ParentId?: string;
    PictureUrl: string;
}
