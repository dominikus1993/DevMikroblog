module Application.Models {
    export class Result<T> {
        IsSuccess: boolean;
        IsWarning: boolean;
        IError: boolean;
        Messages: boolean;
        Value:T;
    }

    export class Post {
        public Id: number;
        public Title: string;
        public Message: string;
        public Date: Date;
        public Rate: number;
        public AuthorId: string;
        public AuthorName: string;
        public Tags:Tag[];
    }

    export class Tag {
        public Id: number;
        public Name:string;
    }

    export class  PostToAdd {
        public Title: string;
        public Message: string;
    }

    export class LoginData {
        public UserName: string;
        public Password: string;
        public isRemember: boolean = true;
    }

    export class RegisterData {
        public Email: string;
        public Password: string;
        public ConfirmPassword: string;
    }
}