

module Application.Services {

    export interface IPostService {
        getAllPost(callback: (data: Models.Result<Models.Post[]>) => void);
        add(post: Models.PostToAdd, callback: (post: Models.Result<Models.Post>) => void);
        delete(id: number, callback: (result: Models.Result<boolean>) => void);
        voteUp(postId: number, callback: (result: Models.Result<Models.Post>) => void);
        voteDown(postId: number, callback: (result: Models.Result<Models.Post>) => void);
        getByTagName(tagName: string, callback: (data: Models.Result<Models.Post[]>) => void);
        getByAuthorName(authorName: string, callback: (data: Models.Result<Models.Post[]>) => void);
    }

    export class PostService implements IPostService {

        private http: ng.IHttpService;

        constructor($http: ng.IHttpService) {
            this.http = $http;
        }

        getAllPost(callback: (data: Models.Result<Models.Post[]>) => void) {
            return this.http.get(Urls.getAllPostUrl).success((data: Models.Result<Models.Post[]>, status) => {
                console.assert(status === 200);
                callback(data);
                return data;
            }).error((error) => {
                callback(error);
                return error;
            });
        }

        getByTagName(tagName: string, callback: (data: Models.Result<Models.Post[]>) => void) {
            this.http.get(Urls.getByTagNameUrl(tagName)).success((data:Models.Result<Models.Post[]>, status) => {
                console.assert(status === 200);
                callback(data);
                return data;
            }).error(error => {
                console.error(error);
                return error;
            });
        }

        getByAuthorName(authorName: string, callback: (data: Models.Result<Models.Post[]>) => void) {
            this.http.get(Urls.getByAuthorNameUrl(authorName)).success((data: Models.Result<Models.Post[]>, status) => {
                console.assert(status === 200);
                callback(data);
                return data;
            }).error(error => {
                console.error(error);
                return error;
            });
        }

        add(post: Models.PostToAdd, callback: (post: Models.Result<Models.Post>) => void) {
            const token = `bearer ${Constants.getTokenValue()}`;

            return this.http({
                method: "POST",
                url: Urls.addPostUrl,
                data: post,
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

        delete(id: number, callback: (result: Models.Result<boolean>) => void) {
            const token = `bearer ${Constants.getTokenValue()}`;

            return this.http({
                method: "DELETE",
                url: `${Urls.deletePostUrl}/${id}`,
                headers: {
                    "Authorization": token
                }
            }).success((data) => {
                console.log(data);
                callback(data as Models.Result<boolean>);
            }).error(error => {
                console.error(error);
            });
        }

        voteUp(postId: number, callback: (result: Models.Result<Models.Post>) => void) {
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

        voteDown(postId: number, callback: (result: Models.Result<Models.Post>) => void) {
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