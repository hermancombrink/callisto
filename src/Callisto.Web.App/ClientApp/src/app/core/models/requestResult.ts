import { RequestStatus } from "./requestStatus";

export class RequestResult {
  result: string = '';
  status: RequestStatus = RequestStatus.Success;
  friendlyMessage: string = '';
}


export class RequestTypedResult<T> {
  result: T;
  status: RequestStatus = RequestStatus.Success;
  friendlyMessage: string = '';
}
