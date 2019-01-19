import { PostUser } from '../models/post.user.model';

export class Post {
    constructor(
        public postId: number = 0,
        public postTitle: string = '',
        public postDate: Date = null,
        public postContent: string = '',
        public postUser: PostUser = new PostUser(),
        // public postCreatorId: number = 0,
        // public postCreatorUserName: string = '',
    ) { }
}