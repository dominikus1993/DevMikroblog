module Application.Urls {
    export const getAllPostUrl = "/api/post";
    export const loginUrl = "/Token";
    export const registerUrl = "/api/Account/Register";
    export const addPostUrl = "api/Post";
    export const deletePostUrl = "api/Post";
    export const postVoteUpUrl = (postId: number) => `post/${postId}/voteup`;
    export const postVoteDownUrl = (postId: number) => `post/${postId}/votedown`;
}