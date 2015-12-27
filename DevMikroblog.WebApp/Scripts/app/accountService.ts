module Application.Services {

    export interface IAccountService {
        login(loginData: Models.LoginData, callback: (data: any) => void);
        register(registerData: Models.RegisterData, callback: (data: any) => void);
        logout(): boolean;
    }

    export class AccountService implements IAccountService {

        public http: ng.IHttpService;

        constructor($http: ng.IHttpService) {
            console.log("account service");
            this.http = $http;
        }

        login(loginData: Models.LoginData, callback: (data: any) => void) {
            const data = `grant_type=password&username=${loginData.UserName}&password=${loginData.Password}`;
            console.log(data);
            this.http.post(Urls.loginUrl, data).success((result: any) => {
                console.log(result);
                if (loginData.isRemember) {
                    localStorage.setItem(Constants.tokenKey, result.access_token);
                    localStorage.setItem(Constants.accountKey, result.userName);
                } else {
                    sessionStorage.setItem(Constants.tokenKey, result.access_token);
                    sessionStorage.setItem(Constants.accountKey, result.userName);
                }
                callback(result);
                return result;
            }).error(error => {
                callback(error);
                return error;
            });
        }

        register(registerData: Models.RegisterData, callback: (data: any) => void) {
            this.http.post(Urls.registerUrl, JSON.stringify(registerData)).success(data => {
                console.log(data);
                return true;
            }).error(error => {
                console.error(error);
                return error;
            });
        }

        logout(): boolean {
            if (sessionStorage.getItem(Constants.tokenKey) && sessionStorage.getItem(Constants.accountKey)) {
                sessionStorage.removeItem(Constants.tokenKey);
                sessionStorage.removeItem(Constants.accountKey);
            }
            else if (localStorage.getItem(Constants.tokenKey) && localStorage.getItem(Constants.accountKey)) {
                localStorage.removeItem(Constants.tokenKey);
                localStorage.removeItem(Constants.accountKey);
            }
            return true;
        }
    }
}