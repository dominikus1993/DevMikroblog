module Application.Models {
    export class Result<T> {
        IsSuccess: boolean;
        IsWarning: boolean;
        IError: boolean;
        Messages: boolean;
        Value: T;
    }

    export class Post {
        public Id: number;
        public Title: string;
        public Message: string;
        public Date: Date;
        public Rate: number;
        public AuthorId: string;
        public AuthorName: string;
        public Tags: Tag[];
        public Votes: Vote[];
        public Comments: Comment[];
    }

    export class PostToUpdate {

        public Id: number;
        public Title: string;
        public Message: string;

        constructor(id?: number, title?: string, message?: string) {
            this.Id = id;
            this.Title = title;
            this.Message = message;
        }
    }

    export class Comment {
        public Id: number;
        public Message: string;
        public Date: Date;
        public Rate: number;
        public AuthorName: string;
        public AuthorId;
        public Votes: Vote[];
    }

    export class CommentToAdd {
        public Message: string;
        public PostId: number;
    }

    export enum UserVote {
        VoteUp,
        VoteDown
    }

    export class Vote {
        public Id: number;
        public UserVote: UserVote;
        public UserId: string;
        public UserName:string;
    }

    export class Tag {
        public Id: number;
        public Name: string;
    }

    export class PostToAdd {
        public Title: string = "";
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