/// <reference path="../typings/angularjs/angular.d.ts"/>
/// <reference path="../typings/jquery/jquery.d.ts"/>
/// <reference path="../typings/underscore/underscore.d.ts"/>

module Application.Controllers {

    export class CommentController {
        
        public commentToAdd: Models.CommentToAdd;
        public comments: Models.Comment[];
        public post: Models.Post;

        private rootScope: ng.IRootScopeService;
        private scope: ng.IScope;
        private service : Services.ICommentService;

        constructor($rootScope: ng.IRootScopeService, $scope: angular.IScope, $service: Services.ICommentService, id:number) {
            this.rootScope = $rootScope;
            this.scope = $scope;
            this.service = $service;
            this.getPostById(id);
        }

        public getPostById(postId: number) {           
            this.service.getPostById(postId, result => {
                if (result.IsSuccess) {
                    this.post = result.Value;
                    this.comments = result.Value.Comments;
                }
            });
        }

        public add() {
            this.commentToAdd.PostId = this.post.Id;
            this.service.add(this.commentToAdd, result => {
                if (result.IsSuccess) {
                    console.log(this.comments);
                    this.comments = this.comments.concat([result.Value]);
                }
            });
        }

        public delete(id: number) {
            this.service.delete(id, (data) => {
                if (data.IsSuccess && data.Value) {
                    this.comments = _.without<Models.Comment>(this.comments, this.comments.filter(x => x.Id === id)[0]);
                }
            });
        }

        public dateAgo(date: string) {
            return Utils.DateUtils.dateAgo(new Date(date));
        }
    }

}