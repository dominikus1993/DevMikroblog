/// <reference path="../typings/angularjs/angular.d.ts"/>
/// <reference path="../typings/jquery/jquery.d.ts"/>
/// <reference path="../typings/underscore/underscore.d.ts"/>

module Application.Controllers {

    export class CommentController {
        
        public commentToAdd: Models.CommentToAdd;
        public comments: Models.Comment[];
        public post: Models.Post;

        private  rootScope: ng.IRootScopeService;
        private scope: ng.IScope;
        private service : Services.ICommentService;

        constructor($rootScope: ng.IRootScopeService, $scope: angular.IScope, $service: Services.ICommentService) {
            this.rootScope = $rootScope;
            this.scope = $scope;
            this.service = $service;
        }

        public getPostById(postId:number) {
            this.service.getPostById(postId, result => {
                if (result.IsSuccess) {
                    this.post = result.Value;
                    this.comments = result.Value.Comments;
                }
            });
        }

        public add() {
            this.service.add(this.commentToAdd, result => {
                if (result.IsSuccess) {
                    this.comments = [result.Value].concat(this.comments);
                }
            });
        }
    }

}