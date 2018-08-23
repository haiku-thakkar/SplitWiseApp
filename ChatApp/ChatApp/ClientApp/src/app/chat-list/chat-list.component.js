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
var ChatListComponent = /** @class */ (function () {
    function ChatListComponent(msgservice, _dataservice, router, route, hubservice) {
        this.msgservice = msgservice;
        this._dataservice = _dataservice;
        this.router = router;
        this.route = route;
        this.hubservice = hubservice;
        this.notifysenders = [];
        this.users = [];
        this.senderupdate = new UserLogin_1.UserLogin();
        this.unread = 1;
        this.countmsg = 1;
    }
    ChatListComponent.prototype.mapcount = function () {
        var _this = this;
        var map = this.notifysenders.reduce(function (prev, cur) {
            prev[cur] = (prev[cur] || 0) + 1;
            return prev;
        }, {});
        // console.log(map);
        this._dataservice.getUsers()
            .subscribe(function (data) {
            _this.users = data;
            for (var key in map) {
                for (var _i = 0, _a = _this.users; _i < _a.length; _i++) {
                    var da = _a[_i];
                    if (da.name == key)
                        da.countmsg = map[key];
                }
            }
        });
    };
    ChatListComponent.prototype.ngOnInit = function () {
        var _this = this;
        var name = this.route.snapshot.params['name'];
        this._dataservice.setsender(name);
        this.sender = this._dataservice.getsender();
        this._dataservice.getUsers()
            .subscribe(function (data) { return _this.users = data; });
        this.interval = setInterval(function () {
            _this.notifysenders = [];
            _this.recevier = _this._dataservice.getrecevier();
            _this._dataservice.getUsers()
                .subscribe(function (data) { return _this.users = data; });
            _this.msgservice.getMsgs().subscribe(function (data) {
                // console.log(data);
                for (var _i = 0, data_1 = data; _i < data_1.length; _i++) {
                    var msg = data_1[_i];
                    if (msg.recevier === _this.sender && msg.isRead == false) {
                        _this.notifysenders.push(msg.sender);
                    }
                }
                // console.log(this.notifysenders)
                _this.mapcount();
            });
        }, 10000);
    };
    ChatListComponent = __decorate([
        core_1.Component({
            selector: 'app-chat-list',
            templateUrl: './chat-list.component.html',
            styleUrls: ['./chat-list.component.css']
        })
    ], ChatListComponent);
    return ChatListComponent;
}());
exports.ChatListComponent = ChatListComponent;
//# sourceMappingURL=chat-list.component.js.map