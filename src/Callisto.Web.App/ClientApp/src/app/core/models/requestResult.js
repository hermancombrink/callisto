"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var requestStatus_1 = require("./requestStatus");
var RequestResult = /** @class */ (function () {
    function RequestResult() {
        this.result = '';
        this.status = requestStatus_1.RequestStatus.Success;
        this.friendlyMessage = '';
    }
    return RequestResult;
}());
exports.RequestResult = RequestResult;
var RequestTypedResult = /** @class */ (function () {
    function RequestTypedResult() {
        this.status = requestStatus_1.RequestStatus.Success;
        this.friendlyMessage = '';
    }
    return RequestTypedResult;
}());
exports.RequestTypedResult = RequestTypedResult;
//# sourceMappingURL=requestResult.js.map