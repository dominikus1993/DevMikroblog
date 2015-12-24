

module Application.Services{
    export interface IPostService {
        getAllPost(callback: (data: Models.Result<Models.Post[]>) => void);
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
    } 
}