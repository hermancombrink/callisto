import { RequestStatus } from "./requestStatus";

export class RequestResult {
  Result: string = '';
  Status: RequestStatus = RequestStatus.Success;
  FriendlyMessage: string = '';
}


export class RequestTypedResult<T> {
  Result: T;
  Status: RequestStatus = RequestStatus.Success;
  FriendlyMessage: string = '';
}
