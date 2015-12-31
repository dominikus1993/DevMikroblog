module Application.Controllers {
    export class AccountController {

        private scope: ng.IScope;
        private service: Services.IAccountService;
        private rootScope: ng.IRootScopeService;
        public loginData: Models.LoginData;
        public registerData: Models.RegisterData;
        public userName:string;

        constructor($rootScope:ng.IRootScopeService, $scope: angular.IScope, $service: Services.IAccountService) {
            this.scope = $scope;
            this.service = $service;
            this.rootScope = $rootScope;
            (this.rootScope as any).isLogged = Constants.checkCredentials();
            this.userName = Constants.getAccountValue();
        }

        public register() {
            console.log(this.registerData);
            this.service.register(this.registerData, (data) => {
                if (data) {
                    console.log(data);
                }
            });
        }

        public login() {
            this.service.login(this.loginData, (data) => {
                (this.rootScope as any).isLogged = Constants.checkCredentials();
                this.userName = Constants.getAccountValue();
            });
        }

        public logout() {
            this.service.logout();
            (this.rootScope as any).isLogged = Constants.checkCredentials();
            this.userName = Constants.getAccountValue();
        }      
    }
}