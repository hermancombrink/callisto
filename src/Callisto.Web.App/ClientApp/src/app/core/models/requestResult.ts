import { RequestStatus } from './requestStatus';

export class RequestResult {
  Result = '';
  Status: RequestStatus = RequestStatus.Success;
  FriendlyMessage = '';
}

export class RequestTypedResult<T> {
  Result: T;
  Status: RequestStatus = RequestStatus.Success;
  FriendlyMessage = '';
}
