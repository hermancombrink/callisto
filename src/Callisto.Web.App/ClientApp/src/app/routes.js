"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var home_component_1 = require("./home/home.component");
var auth_guard_1 = require("./core/auth.guard");
exports.AppRoutes = [
    { path: '', component: home_component_1.HomeComponent, pathMatch: 'full', canActivate: [auth_guard_1.AuthGuard] }
];
//# sourceMappingURL=routes.js.map