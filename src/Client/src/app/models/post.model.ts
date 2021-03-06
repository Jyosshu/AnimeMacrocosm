import { User } from '../models/post.user.model';

export class Post {
    constructor(
        public postId: number = 0,
        public postTitle: string = '',
        public postDate: Date = null,
        public postContent: string = '',
        
        public user: User = new User(),
    ) { }
}