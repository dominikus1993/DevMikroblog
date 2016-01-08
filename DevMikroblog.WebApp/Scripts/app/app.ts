/// <reference path="../typings/angularjs/angular.d.ts"/>
/// <reference path="../typings/angularjs/angular-route.d.ts"/>

var appModule = angular.module("DevMikroblog", ["ngRoute", "ngMessages", "ngSanitize"]);

appModule.value("isLogged", Application.Constants.checkCredentials());
appModule.controller("PostController", ["$rootScope", "$scope", "PostService", ($rootScope, $scope, postService) => new Application.Controllers.PostConroller($rootScope, $scope, postService, Application.Controllers.PostMode.AllPost)]);
appModule.controller("TagPostController", ["$rootScope", "$scope", "$routeParams", "PostService", ($rootScope, $scope, $routeParams, postService) => new Application.Controllers.PostConroller($rootScope, $scope, postService, Application.Controllers.PostMode.PostsByTag, $routeParams.tagName)]);
appModule.controller("AuthorNamePostController", ["$rootScope", "$scope", "$routeParams", "PostService", ($rootScope, $scope, $routeParams, postService) => new Application.Controllers.PostConroller($rootScope, $scope, postService, Application.Controllers.PostMode.PostsByAuthorName, $routeParams.authorName)]);
appModule.controller("AccountController", ["$rootScope", "$scope", "AccountService", ($rootScope, $scope, accountService) => new Application.Controllers.AccountController($rootScope, $scope, accountService)]);
appModule.controller("CommentController", ["$rootScope", "$scope", "$routeParams", "CommentService", ($rootScope, $scope, $routeParams, commentService) => new Application.Controllers.CommentController($rootScope, $scope, commentService, $routeParams.postId)]);

appModule.factory("PostService", ["$http", ($http) => new Application.Services.PostService($http)]);
appModule.factory("AccountService", ["$http", ($http) => new Application.Services.AccountService($http)]);
appModule.factory("CommentService", ["$http", ($http) => new Application.Services.CommentService($http)]);

appModule.config(["$routeProvider", "$locationProvider", ($routeProvider, $locationProvider) => Application.Routes.Routing.get($locationProvider, $routeProvider)])