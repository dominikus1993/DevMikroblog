/// <reference path="../typings/angularjs/angular.d.ts"/>
/// <reference path="../typings/jquery/jquery.d.ts"/>
/// <reference path="../typings/underscore/underscore.d.ts"/>

module Application.Controllers {

    export class CommentController {
        
        public commentToAdd: Models.CommentToAdd;
        public postToUpdate:Models.PostToUpdate;
        public comments: Models.Comment[];
        public post: Models.Post;
        public toUpdate = false;
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
                    result.Value.Message = Utils.TagUtils.parseTag(result.Value.Message);
                    this.post = result.Value;
                    this.comments = result.Value.Comments;
                }
            });
        }

        public add() {
            this.commentToAdd.PostId = this.post.Id;
            this.service.add(this.commentToAdd, result => {
                if (result.IsSuccess) {
                    this.comments = this.comments.concat([result.Value]);
                }
            });
        }

        public edit() {
            this.service.update(this.postToUpdate, (result) => {
                if (result.IsSuccess) {
                    result.Value.Message = Utils.TagUtils.parseTag(result.Value.Message);
                    this.post = result.Value;
                    this.comments = result.Value.Comments;
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

        public isEdited() {
            this.postToUpdate = new Models.PostToUpdate(this.post.Id, this.post.Title, this.post.Message);
            this.toUpdate = !this.toUpdate;
        }

        public isOwner() {
            const userName = Constants.getAccountValue();
            if (userName && this.post) {
                return userName === this.post.AuthorName;
            }
            return false;
        }
    }

}