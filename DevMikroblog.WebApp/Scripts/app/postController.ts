/// <reference path="../typings/angularjs/angular.d.ts"/>
/// <reference path="../typings/jquery/jquery.d.ts"/>
/// <reference path="../typings/underscore/underscore.d.ts"/>

module Application.Controllers {

    export enum PostMode {
        AllPost,
        PostsByTag,
        PostsByAuthorName,
    }

    export class PostConroller {
        public scope: any;
        public service: Services.IPostService;
        public rootService: ng.IRootScopeService;
        public posts: Models.Post[];
        public postToAdd: Models.PostToAdd;

        constructor($rootScope: ng.IRootScopeService, $scope: ng.IScope, $service: Services.IPostService, mode: PostMode, searchPattern?: string) {
            this.scope = $scope;
            this.rootService = $rootScope;
            this.service = $service;
            this.posts = [];
            console.log(`${mode} ${searchPattern}`);
            this.getByMode(mode, searchPattern);
        }

        public getByMode(mode: PostMode, searchPattern?: string) {
            switch (mode) {
                case PostMode.AllPost:
                    this.getAll();
                    break;
                case PostMode.PostsByTag:
                    this.getByTagName(searchPattern);
                    break;
                case PostMode.PostsByAuthorName:
                    this.getByAuthorName(searchPattern);
                    break;
                default:
            }
        }

        public getAll() {
            this.service.getAllPost((data) => {
                if (data.IsSuccess) {
                    this.posts = data.Value.reverse().map(post => {
                        post.Message = Utils.TagUtils.parseTag(post.Message);
                        return post;
                    });
                }

            });
        }

        public getByTagName(tagName: string) {
            this.service.getByTagName(tagName, result => {
                if (result.IsSuccess) {
                    this.posts = result.Value.reverse();
                }
            });
        }

        public getByAuthorName(authorName: string) {
            this.service.getByAuthorName(authorName, result => {
                if (result.IsSuccess) {
                    this.posts = result.Value.reverse();
                }
            });
        }

        public add() {
            if (this.postToAdd && this.postToAdd.Message && this.postToAdd.Title) {
                this.service.add(this.postToAdd, (data) => {
                    if (data.IsSuccess) {
                        this.posts = [data.Value].concat(this.posts);
                    }
                });
            } else {
                console.log("Nope");
            }

        }

        public delete(id: number) {
            this.service.delete(id, result => {
                if (result.IsSuccess && result.Value) {
                    console.log("Deleted");
                    this.posts = _.without<Models.Post>(this.posts, this.posts.filter(x => x.Id === id)[0]);
                }
            });
        }

        public isOwner(authorName: number) {
            const userName = Constants.getAccountValue();
            if (userName) {
                return userName === authorName;
            }
            return false;
        }

        public voteUp(postId: number) {
            console.log(postId);
            this.service.voteUp(postId, (result) => {
                if (result.IsSuccess) {
                    this.posts = this.posts.map(post => {
                        if (result.Value.Id === post.Id) {
                            return result.Value;
                        }
                        return post;
                    });
                }
            });
        }

        public voteDown(postId: number) {
            this.service.voteDown(postId, (result) => {
                if (result.IsSuccess) {
                    this.posts.filter(x => x.Id === result.Value.Id)[0] = result.Value;
                }
            });
        }

        public postWasVoted(postId: number, userId: string): boolean {
            return this.posts.filter(post => post.Id === postId && post.Votes.filter(vote => vote.UserId === userId).length > 0).length > 0;
        }

        public userVotedUp(postId: number, userId: string):boolean {
            const post = _.find(this.posts, x => x.Id === postId);
            return post.Votes.filter(vote => vote.UserId === userId).length > 0;
        }

        public dateAgo(date:string) {
            return Utils.DateUtils.dateAgo(new Date(date));
        }

    }
}