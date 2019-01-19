export class PostUser {
    constructor (
        public userId: number = 0,
        public userEmailAddress: string = '',
        public userScreenName: string = ''
    ) {}
}