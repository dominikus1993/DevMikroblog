﻿/// <reference path="../typings/angularjs/angular.d.ts"/>
/// <reference path="../typings/jquery/jquery.d.ts"/>
/// <reference path="../typings/underscore/underscore.d.ts"/>

module Application.Controllers {

    export enum PostMode {
        AllPost,
        PostByTag
    }

    export class PostConroller {
        public scope: any;
        public service: Services.IPostService;
        public rootService: ng.IRootScopeService;
        public posts: Models.Post[];
        public postToAdd: Models.PostToAdd;

        constructor($rootScope: ng.IRootScopeService, $scope: ng.IScope, $service: Services.IPostService, mode: PostMode, tagName: string = null) {
            this.scope = $scope;
            this.rootService = $rootScope;
            this.service = $service;
            this.posts = [];
            console.log(`${mode} ${tagName}`);
            this.getByMode(mode, tagName);
        }

        public getByMode(mode: PostMode, tagName: string = null) {
            switch (mode) {
                case PostMode.AllPost:
                    this.getAll();
                    break;
                case PostMode.PostByTag:
                    this.getByTagName(tagName);
                    break;
                default:
            }
        }

        public getAll() {
            this.service.getAllPost((data) => {
                if (data.IsSuccess) {
                    this.posts = data.Value.reverse();
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
            this.service.voteUp(postId, (result) => {
                if (result.IsSuccess) {
                    this.posts.filter(x => x.Id === result.Value.Id)[0] = result.Value;
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

    }
}