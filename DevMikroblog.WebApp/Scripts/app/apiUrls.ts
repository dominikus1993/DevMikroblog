module Application.Urls {
    export const getAllPostUrl = "/api/post";
    export const loginUrl = "/Token";
    export const registerUrl = "/api/Account/Register";
    export const addPostUrl = "api/Post";
    export const deletePostUrl = "api/Post";
    export const postVoteUpUrl = (postId: number) => `api/vote/post/${postId}/voteup`;
    export const postVoteDownUrl = (postId: number) => `api/vote/post/${postId}/votedown`;
    export const getByTagNameUrl = (tagName: string) => `api/Post?tagName=${tagName}`;
    export const getByAuthorNameUrl = (authorName: string) => `api/Post?authorName=${authorName}`;
    export const addCommentUrl = "/api/Comment";
    export const getPostByIdUrl = (postId: number) => `/api/Post/${postId}`;
    export const deleteCommentUrl = (commentId:number) => `/api/Comment/${commentId}`;
}