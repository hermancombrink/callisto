"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var requestStatus_1 = require("./requestStatus");
var RequestResult = /** @class */ (function () {
    function RequestResult() {
        this.Result = '';
        this.Status = requestStatus_1.RequestStatus.Success;
        this.FriendlyMessage = '';
    }
    return RequestResult;
}());
exports.RequestResult = RequestResult;
var RequestTypedResult = /** @class */ (function () {
    function RequestTypedResult() {
        this.Status = requestStatus_1.RequestStatus.Success;
        this.FriendlyMessage = '';
    }
    return RequestTypedResult;
}());
exports.RequestTypedResult = RequestTypedResult;
//# sourceMappingURL=requestResult.js.map