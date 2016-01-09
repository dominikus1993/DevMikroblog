module Application.Services {

    export interface ICommentService {
        add(commentToAdd: Models.CommentToAdd, callback: (data: Models.Result<Models.Comment>) => void);
        getPostById(postId: number, callback: (data: Models.Result<Models.Post>) => void);
        getPostComments(postId: number, callback: (data: Models.Result<Models.Comment[]>) => void);
        delete(commentId: number, callback: (data: Models.Result<boolean>) => void);
        update(postToUpdate: Models.PostToUpdate, callback: (result: Models.Result<Models.Post>) => void);
        voteUp(commentId: number, callback: (result: Models.Result<Models.Comment>) => void);
        voteDown(commentId: number, callback: (result: Models.Result<Models.Comment>) => void);
        postVoteUp(postId: number, callback: (result: Models.Result<Models.Post>) => void);
    }

    export class CommentService implements ICommentService {


        private http: ng.IHttpService;

        constructor($http: angular.IHttpService) {
            this.http = $http;
        }

        public getPostById(postId: number, callback: (data: Models.Result<Models.Post>) => void) {
            this.http.get(Urls.getPostByIdUrl(postId)).success((data: Models.Result<Models.Post>, status) => {
                console.assert(status === 200);
                callback(data);
            });
        }

        public delete(commentId: number, callback: (data: Models.Result<boolean>) => void) {
            this.http.get(Urls.deleteCommentUrl(commentId)).success((data: Models.Result<boolean>, status) => {
                console.assert(status === 200);
                callback(data);
            }).error((error) => {
                console.error(error);
            });
        }

        public add(commentToAdd: Models.CommentToAdd, callback: (data: Models.Result<Models.Comment>) => void) {
            const token = `bearer ${Constants.getTokenValue()}`;

            return this.http({
                method: "POST",
                url: Urls.addCommentUrl,
                data: commentToAdd,
                headers: {
                    "Authorization": token
                }
            }).success((data: Models.Result<Models.Comment>) => {
                console.log(data);
                callback(data);
            }).error(error => {
                console.error(error);
            });
        }

        public update(postToUpdate: Models.PostToUpdate, callback: (result: Models.Result<Models.Post>) => void) {
            const token = `bearer ${Constants.getTokenValue()}`;

            return this.http.put(Urls.updatePostUrl, postToUpdate, {
                headers: {
                    "Authorization": token
                }
            }).success((data: Models.Result<Models.Post>) => {
                console.log(data);
                callback(data);
            }).error(error => {
                console.error(error);
            });
        }

        voteUp(commentId: number, callback: (result: Models.Result<Models.Comment>) => void) {
            const token = `bearer ${Constants.getTokenValue()}`;

            return this.http({
                method: "GET",
                url: Urls.commentVoteUpUrl(commentId),
                headers: {
                    "Authorization": token
                }
            }).success((data) => {
                console.log(data);
                callback(data as Models.Result<Models.Comment>);
            }).error(error => {
                console.error(error);
            });
        }

        voteDown(commentId: number, callback: (result: Models.Result<Models.Comment>) => void) {
            const token = `bearer ${Constants.getTokenValue()}`;

            return this.http({
                method: "GET",
                url: Urls.commentVoteUpUrl(commentId),
                headers: {
                    "Authorization": token
                }
            }).success((data) => {
                console.log(data);
                callback(data as Models.Result<Models.Comment>);
            }).error(error => {
                console.error(error);
            });
        }

        getPostComments(postId: number, callback: (data: Models.Result<Models.Comment[]>) => void) {
            return this.http.get(Urls.getCommentsByPost(postId)).success((data: Models.Result<Models.Comment[]>) => {
                callback(data);
                return data;;
            }).error(error => {
                console.error(error);
                return error;
            });
        }

        postVoteUp(postId: number, callback: (result: Models.Result<Models.Post>) => void) {
            const token = `bearer ${Constants.getTokenValue()}`;

            return this.http({
                method: "GET",
                url: Urls.postVoteUpUrl(postId),
                headers: {
                    "Authorization": token
                }
            }).success((data) => {
                console.log(data);
                callback(data as Models.Result<Models.Post>);
            }).error(error => {
                console.error(error);
            });
        }
    }
}


