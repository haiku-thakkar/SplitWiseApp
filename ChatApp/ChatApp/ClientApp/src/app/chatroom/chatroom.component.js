"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var UserLogin_1 = require("../Model/UserLogin");
var ChatroomComponent = /** @class */ (function () {
    function ChatroomComponent(route, _dataservice, hubservice, _msgservice) {
        this.route = route;
        this._dataservice = _dataservice;
        this.hubservice = hubservice;
        this._msgservice = _msgservice;
        this.message = '';
        this.messages = [];
        this.hismsg = [];
        this.addMessage = new UserLogin_1.Messages();
        hubservice.messages = [];
    }
    ChatroomComponent.prototype.onSubmit = function () {
        var _this = this;
        this.dateTimeLocal = new Date().toLocaleString();
        this.sender = this._dataservice.getsender();
        this._dataservice.getUser(this.recevier)
            .subscribe(function (data) {
            _this.recevierid = data.connectionID,
                _this._dataservice.getUser(_this.sender)
                    .subscribe(function (data1) {
                    _this.senderid = data1.connectionID,
                        _this.msg = _this.message + ":" + _this.dateTimeLocal;
                    _this.messages = _this.hubservice.senddirectmsg(_this.recevierid, _this.senderid, _this.msg, _this.sender);
                });
        });
        this.addMessage.message = this.message;
        this.addMessage.sender = this.sender;
        this.addMessage.recevier = this.recevier;
        this.addMessage.time = this.dateTimeLocal;
        this.addMessage.IsRead = false;
        this._msgservice.addMsg(this.addMessage).subscribe(function (data) { return console.log(data); });
    };
    ChatroomComponent.prototype.ngOnInit = function () {
        var _this = this;
        console.log(this.dateTimeLocal);
        this.sender = this._dataservice.getsender();
        this.name = this.route.snapshot.params['name'];
        this._dataservice.setreceiver(this.name);
        this.recevier = this._dataservice.getrecevier();
        console.log("sender:" + this.sender);
        console.log("recevier:" + this.recevier);
        this._msgservice.getMsgs().subscribe(function (data) {
            for (var _i = 0, data_1 = data; _i < data_1.length; _i++) {
                var msg = data_1[_i];
                if (msg.recevier === _this.recevier
                    && msg.sender === _this.sender
                    || msg.recevier === _this.sender &&
                        msg.sender === _this.recevier) {
                    var text = msg.sender + ":" + msg.message + ":" + msg.time;
                    _this.hismsg.push(text);
                    msg.isRead = true;
                    _this._msgservice.update(msg).subscribe(function (data) { return console.log(data); });
                }
            }
        });
    };
    __decorate([
        core_1.Input()
    ], ChatroomComponent.prototype, "sender", void 0);
    ChatroomComponent = __decorate([
        core_1.Component({
            selector: 'app-chatroom',
            templateUrl: './chatroom.component.html',
            styleUrls: ['./chatroom.component.css']
        })
    ], ChatroomComponent);
    return ChatroomComponent;
}());
exports.ChatroomComponent = ChatroomComponent;
//# sourceMappingURL=chatroom.component.js.map