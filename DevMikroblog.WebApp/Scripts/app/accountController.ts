module Application.Controllers {
    export class AccountController {

        private scope: ng.IScope;
        private service: Services.IAccountService;
        public loginData: Models.LoginData;
        public registerData: Models.RegisterData;
        public isLogged: boolean;

        constructor($scope: angular.IScope, $service: Services.IAccountService) {
            this.scope = $scope;
            this.service = $service;
            this.isLogged = this.checkCredentials();
            console.log("dsdsd");
        }

        public login() {
            console.log("wywolanie");
            this.service.login(this.loginData, (data) => {
                console.log(data);
                this.checkCredentials();
            });
        }

        private checkCredentials() {
            return (sessionStorage.getItem(Constants.accountKey) && sessionStorage.getItem(Constants.tokenKey)) || (localStorage.getItem(Constants.accountKey) && localStorage.getItem(Constants.tokenKey)) 
        }
        
    }
}