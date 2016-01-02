/// <reference path="../typings/angularjs/angular.d.ts"/>
/// <reference path="../typings/angularjs/angular-route.d.ts"/>

module Application.Routes {
    export class Routing {
        static get(locationProvider: angular.ILocationProvider, routeProvider: angular.route.IRouteProvider) {
            console.log("route2");
            routeProvider.when("/", { templateUrl: "Home/All", controller: "PostController", controllerAs: "pc" });
            routeProvider.when("/Tag/:tagName?", { templateUrl: `/Home/Tag/`, controller: "TagPostController", controllerAs:"pc" });
            routeProvider.otherwise({redirectTo: "/"});

            locationProvider.html5Mode(false).hashPrefix("!");
        }
    }
}