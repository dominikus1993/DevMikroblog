/// <reference path="../typings/angularjs/angular.d.ts"/>
/// <reference path="../typings/angularjs/angular-route.d.ts"/>

var appModule = angular.module("DevMikroblog", ["ngRoute", "ngMessages"]);

appModule.value("isLogged", Application.Constants.checkCredentials());
appModule.controller("PostController", ["$rootScope", "$scope", "PostService", ($rootScope, $scope, postService) => new Application.Controllers.PostConroller($rootScope, $scope, postService, Application.Controllers.PostMode.AllPost)]);
appModule.controller("TagPostController", ["$rootScope", "$scope", "$routeParams", "PostService", ($rootScope, $scope, $routeParams, postService) => new Application.Controllers.PostConroller($rootScope, $scope, postService, Application.Controllers.PostMode.PostByTag, $routeParams.tagName)]);
appModule.controller("AccountController", ["$rootScope", "$scope", "AccountService", ($rootScope, $scope, accountService) => new Application.Controllers.AccountController($rootScope, $scope, accountService)]);

appModule.factory("PostService", ["$http", ($http) => new Application.Services.PostService($http)]);
appModule.factory("AccountService", ["$http", ($http) => new Application.Services.AccountService($http)])

appModule.config(["$routeProvider", "$locationProvider", ($routeProvider, $locationProvider) => Application.Routes.Routing.get($locationProvider, $routeProvider)])