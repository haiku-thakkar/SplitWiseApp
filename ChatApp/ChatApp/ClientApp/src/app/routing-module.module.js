"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var common_1 = require("@angular/common");
var chat_list_component_1 = require("./chat-list/chat-list.component");
var router_1 = require("@angular/router");
var chatroom_component_1 = require("./chatroom/chatroom.component");
var forms_1 = require("@angular/forms");
var HubConnectionService_1 = require("./Service/HubConnectionService");
var MessageService_1 = require("./Service/MessageService");
var appRoutes = [{
        path: 'list/:name',
        component: chat_list_component_1.ChatListComponent,
        children: [
            { path: 'room/:name', component: chatroom_component_1.ChatroomComponent }
        ]
    }];
var RoutingModuleModule = /** @class */ (function () {
    function RoutingModuleModule() {
    }
    RoutingModuleModule = __decorate([
        core_1.NgModule({
            imports: [
                common_1.CommonModule,
                forms_1.FormsModule,
                router_1.RouterModule.forRoot(appRoutes)
            ],
            declarations: [
                chat_list_component_1.ChatListComponent,
                chatroom_component_1.ChatroomComponent
            ],
            providers: [HubConnectionService_1.HubConnectionService,
                MessageService_1.MessageService]
        })
    ], RoutingModuleModule);
    return RoutingModuleModule;
}());
exports.RoutingModuleModule = RoutingModuleModule;
//# sourceMappingURL=routing-module.module.js.map