/// <reference path="../typings/angularjs/angular.d.ts"/>
/// <reference path="../typings/jquery/jquery.d.ts"/>

module Application.Controllers {

    export class PostConroller {
        public scope: any;
        public service: Services.IPostService;
        public rootService: ng.IRootScopeService;
        public posts: Models.Post[];
        public postToAdd: Models.PostToAdd;

        constructor($rootScope: ng.IRootScopeService, $scope: ng.IScope, $service: Services.IPostService) {
            this.scope = $scope;
            this.rootService = $rootScope;
            this.service = $service;
            this.posts = [];
            this.getAll();
        }

        public getAll() {
            this.service.getAllPost((data) => {
                if (data.IsSuccess) {
                    this.posts = data.Value;
                }

            });
        }

        public add() {
            if (this.postToAdd && this.postToAdd.Message && this.postToAdd.Title) {
                this.service.add(this.postToAdd, (data) => {
                    if (data.IsSuccess) {
                        this.posts.push(data.Value);
                    }
                });
            } else {
                console.log("Nope");
            }

        }

    }
}