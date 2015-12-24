/// <reference path="../typings/angularjs/angular.d.ts"/>
/// <reference path="../typings/jquery/jquery.d.ts"/>

module Application.Controllers {
    
    export class PostConroller {
        public scope: any;
        public service:Services.IPostService;
        public posts: Models.Post[];

        constructor($scope: ng.IScope, $service:Services.IPostService) {
            this.scope = $scope;
            this.service = $service;
            this.posts = [];
            console.log("constructor");
            this.getAll();
        }

        public getAll() {
            console.log("Siema");
            this.service.getAllPost((data) => {
                if (data.IsSuccess) {
                    console.log("sukces");
                    this.posts = data.Value;
                }

            });         
        }

    }
}