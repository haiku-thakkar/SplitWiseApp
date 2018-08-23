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
        this.accessPointUrl = '/api/message';
    }
    MessageService.prototype.addMsg = function (msg) {
        return this.http.post(this.accessPointUrl, msg);
    };
    MessageService.prototype.getMsgs = function () {
        return this.http.get(this.accessPointUrl);
    };
    MessageService.prototype.update = function (msg) {
        return this.http.put(this.accessPointUrl + '/' + msg.id, msg);
    };
    MessageService = __decorate([
        core_1.Injectable()
    ], MessageService);
    return MessageService;
}());
exports.MessageService = MessageService;
//# sourceMappingURL=MessageService.js.map