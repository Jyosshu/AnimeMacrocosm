import { SeriesItem } from './seriesItem.model';
import { CreatorAuthor } from './creatorAuthor.model';

export class SeriesSummary {
    constructor(
        public seriesId: number = 0,
        public title: string = '',
        public creatorAuthors: CreatorAuthor[],
        public seriesItems: SeriesItem[]
    ) { }
}