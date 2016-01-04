module Application.Services {

    export interface ICommentService {
        add(commentToAdd: Models.CommentToAdd, callback: (data: Models.Result<Models.Comment>) => void);
        getPostById(postId: number, callback: (data: Models.Result<Models.Post>) => void);
        delete(commentId: number, callback: (data: Models.Result<boolean>) => void);
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
    }

}