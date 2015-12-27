module Application.Constants {
    export const tokenKey = "userToken";
    export const accountKey = "accountData";

    export const getAccountValue = () => {
        if (sessionStorage.getItem(Constants.accountKey)) {
            return sessionStorage.getItem(Constants.accountKey);
        }
        else if (localStorage.getItem(Constants.accountKey)) {
            return localStorage.getItem(Constants.accountKey);
        }
        return "";
    }

    export const getTokenValue = () => {
        if (sessionStorage.getItem(Constants.tokenKey)) {
            return sessionStorage.getItem(Constants.tokenKey);
        }
        else if (localStorage.getItem(Constants.tokenKey)) {
            return localStorage.getItem(Constants.tokenKey);
        }
        return "";
    }

    export const checkCredentials = () => {
        return (sessionStorage.getItem(Constants.accountKey) != null && sessionStorage.getItem(Constants.tokenKey) != null) || (localStorage.getItem(Constants.accountKey) != null && localStorage.getItem(Constants.tokenKey) != null);
    }
}