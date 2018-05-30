"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
var AssetViewModel = /** @class */ (function () {
    function AssetViewModel() {
    }
    return AssetViewModel;
}());
exports.AssetViewModel = AssetViewModel;
var AssetInfoViewModel = /** @class */ (function (_super) {
    __extends(AssetInfoViewModel, _super);
    function AssetInfoViewModel() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return AssetInfoViewModel;
}(AssetViewModel));
exports.AssetInfoViewModel = AssetInfoViewModel;
var AssetTreeViewModel = /** @class */ (function (_super) {
    __extends(AssetTreeViewModel, _super);
    function AssetTreeViewModel() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return AssetTreeViewModel;
}(AssetViewModel));
exports.AssetTreeViewModel = AssetTreeViewModel;
var AssetDetailViewModel = /** @class */ (function (_super) {
    __extends(AssetDetailViewModel, _super);
    function AssetDetailViewModel() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return AssetDetailViewModel;
}(AssetViewModel));
exports.AssetDetailViewModel = AssetDetailViewModel;
//# sourceMappingURL=assetViewModel.js.map