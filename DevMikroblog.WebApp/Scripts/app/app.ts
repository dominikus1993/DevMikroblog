/// <reference path="../typings/angularjs/angular.d.ts"/>

var appModule = angular.module("DevMikroblog", ['ngMessages']);

appModule.value("isLogged", Application.Constants.checkCredentials());
appModule.controller("PostController", ["$rootScope","$scope", "PostService", ($rootScope, $scope, postService) => new Application.Controllers.PostConroller($rootScope, $scope, postService)]);
appModule.controller("AccountController", ["$rootScope", "$scope", "AccountService", ($rootScope, $scope, accountService) => new Application.Controllers.AccountController($rootScope, $scope, accountService)]);


appModule.factory("PostService", ["$http", ($http) => new Application.Services.PostService($http)]);
appModule.factory("AccountService", ["$http", ($http) => new Application.Services.AccountService($http)])