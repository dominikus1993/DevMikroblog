

module Application.Services {

    export interface IPostService {
        getAllPost(callback: (data: Models.Result<Models.Post[]>) => void);
        add(post: Models.PostToAdd, callback: (post: Models.Result<Models.Post>) => void);
        delete(id: number, callback: (result: Models.Result<boolean>) => void);
        voteUp(postId: number, callback: (result: Models.Result<Models.Post>) => void);
        voteDown(postId: number, callback: (result: Models.Result<Models.Post>) => void);
    }

    export class PostService implements IPostService {

        private http: ng.IHttpService;

        constructor($http: ng.IHttpService) {
            this.http = $http;
        }

        getAllPost(callback: (data: Models.Result<Models.Post[]>) => void) {
            return this.http.get(Urls.getAllPostUrl).success((data, status) => {
                console.assert(status === 200);
                console.log(data);
                callback(data as Models.Result<Models.Post[]>);
                return data as Models.Result<Models.Post[]>;
            }).error((error) => {
                callback(error);
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