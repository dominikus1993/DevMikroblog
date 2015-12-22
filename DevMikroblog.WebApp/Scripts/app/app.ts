/// <reference path="../typings/angularjs/angular.d.ts"/>

var appModule = angular.module("DevMikroblog", []);

appModule.controller("PostController", ["$scope", "PostService", ($scope, postService) => new Application.Controllers.PostConroller($scope, postService)]);

appModule.factory("PostService", ["$http", ($http) => new Application.Services.PostService($http)]);