import { Series } from './series.model';
import { CreatorAuthor } from './creatorAuthor.model';

export class SeriesCreator {
    constructor(
        public seriesId: number = 0,
        public series: Series ,
        public creatorId: number = 0,
        public creatorAuthor: CreatorAuthor = new CreatorAuthor()
    ) { }
}