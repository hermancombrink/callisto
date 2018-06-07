export class NewAccountViewModel {
    public FirstName = '';
    public LastName = '';
    public UserRole = '';
    public CompanyDetails = new NewCompanyViewModel();
}

export class NewCompanyViewModel {
    public CompanyName = '';
    public CompanyWebsite = '';
    public CompanySize = '';
}
