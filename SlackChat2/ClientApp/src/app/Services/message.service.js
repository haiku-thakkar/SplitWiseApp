"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var MessageService = /** @class */ (function () {
    function MessageService(http) {
        this.http = http;
    }
    MessageService.prototype.create = function (message) {
        console.log("In create message method", message);
        return this.http.post('/api/message', message)
            .map(function (res) { return res.json(); });
    };
    MessageService.prototype.getmessage = function (id) {
        console.log("in getmessage");
        return this.http.get('/api/message/' + id)
            .map(function (res) { return res.json(); });
    };
    MessageService.prototype.delete = function (id) {
        return this.http.delete('/api/message/' + id)
            .map(function (res) { return res.json(); });
    };
    MessageService.prototype.getMessages = function () {
        console.log("in getmessages");
        return this.http.get('/api/message')
            .map(function (res) { return res.json(); });
    };
    MessageService = __decorate([
        core_1.Injectable()
    ], MessageService);
    return MessageService;
}());
exports.MessageService = MessageService;
//# sourceMappingURL=message.service.js.map