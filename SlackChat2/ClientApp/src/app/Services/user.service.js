"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var UserService = /** @class */ (function () {
    function UserService(http) {
        this.http = http;
    }
    UserService.prototype.create = function (user) {
        console.log("In create method");
        console.log(user.connectionId);
        return this.http.post('/api/users', user)
            .map(function (res) { return res.json(); });
    };
    UserService.prototype.getuser = function (id) {
        console.log("in getuser");
        return this.http.get('/api/users/' + id)
            .map(function (res) { return res.json(); });
    };
    UserService.prototype.update = function (user) {
        console.log(user);
        return this.http.put('/api/users/' + user.id, user)
            .map(function (res) { return res.json(); });
    };
    UserService.prototype.delete = function (id) {
        return this.http.delete('/api/users/' + id)
            .map(function (res) { return res.json(); });
    };
    UserService.prototype.getUsers = function () {
        console.log("in getusers");
        return this.http.get('/api/users')
            .map(function (res) { return res.json(); });
    };
    UserService.prototype.createMsg = function (user, connId, data) {
        console.log("In create message method" + user, data);
        return this.http.post('/api/message', user)
            .map(function (res) { return res.json(); });
    };
    UserService.prototype.getmsg = function () {
        return this.message;
    };
    UserService = __decorate([
        core_1.Injectable()
    ], UserService);
    return UserService;
}());
exports.UserService = UserService;
//# sourceMappingURL=user.service.js.map