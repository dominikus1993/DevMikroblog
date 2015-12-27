module Application.Controllers {
    export class AccountController {

        private scope: ng.IScope;
        private service: Services.IAccountService;
        public loginData: Models.LoginData;
        public registerData: Models.RegisterData;
        public userName:string;

        constructor($scope: angular.IScope, $service: Services.IAccountService) {
            this.scope = $scope;
            this.service = $service;
            (this.scope as any).isLogged = Constants.checkCredentials();
            this.userName = Constants.getAccountValue();
        }

        public login() {
            console.log("wywolanie");
            this.service.login(this.loginData, (data) => {
                console.log(data);
                (this.scope as any).isLogged = Constants.checkCredentials();
                this.userName = Constants.getAccountValue();
            });
        }

        public logout() {
            this.service.logout();
            (this.scope as any).isLogged = Constants.checkCredentials();
            this.userName = Constants.getAccountValue();
        }      
    }
}