/// <reference path="../typings/angularjs/angular.d.ts"/>

var appModule = angular.module("DevMikroblog", []);

appModule.controller("PostController", ["$scope", "PostService", ($scope, postService) => new Application.Controllers.PostConroller($scope, postService)]);
appModule.controller("AccountController", ["$scope", "AccountService", ($scope, accountService) => new Application.Controllers.AccountController($scope,accountService)])

appModule.factory("PostService", ["$http", ($http) => new Application.Services.PostService($http)]);
appModule.factory("AccountService", ["$http", ($http) => new Application.Services.AccountService($http)])