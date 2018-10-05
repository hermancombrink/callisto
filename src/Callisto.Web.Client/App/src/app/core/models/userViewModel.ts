export class UserViewModel {
  public Email: string;
  public UserType: UserType;
  public FirstName: string;
  public LastName: string;
  public Company: string;
  public SubscriptionId: string;
  public ProfileCompleted: boolean;
  public CompanyProfileCompleted: boolean;
}

export enum UserType {
  Member = 0,
  Vendor = 1,
  Customer = 2
}
