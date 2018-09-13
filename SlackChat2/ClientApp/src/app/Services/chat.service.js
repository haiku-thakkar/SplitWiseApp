"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
require("rxjs/add/operator/map");
var signalR = require("@aspnet/signalr");
var ChatService = /** @class */ (function () {
    function ChatService(http) {
        var _this = this;
        this.http = http;
        this.messages = [];
        this._hubConnection = new signalR.HubConnectionBuilder()
            .withUrl('https://localhost:44316/chathub')
            .configureLogging(signalR.LogLevel.Information)
            .build();
        this._hubConnection.start().then(function () {
            _this._hubConnection.invoke('upData');
        }).catch(function (err) { return console.error(err.toString()); });
        this._hubConnection.on('send', function (data) {
            var received = " " + data;
            _this.message = received;
            console.log('received:', _this.message);
            _this.messages.push(received);
            console.log(_this.messages);
            return _this.messages;
        });
        this._hubConnection.on('upData', function (data) {
            _this.connId = data;
            console.log('conn id: ', _this.connId);
        });
    }
    ChatService.prototype.sendMessage = function (data, connId) {
        if (this._hubConnection) {
            console.log('calling send', connId);
            var temp = this._hubConnection.invoke('Send', data, connId);
            console.log("temp", temp);
            return this.messages;
        }
    };
    ChatService = __decorate([
        core_1.Injectable()
    ], ChatService);
    return ChatService;
}());
exports.ChatService = ChatService;
//# sourceMappingURL=chat.service.js.map