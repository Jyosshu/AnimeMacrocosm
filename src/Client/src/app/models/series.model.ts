import { SeriesCreator } from './seriesCreator.model';
import { SeriesItem } from './seriesItem.model';

export class Series {
    constructor(
        public seriesId: number = 0,
        public title: string = '',
        public seriesCreators: SeriesCreator[],
        public seriesItems: SeriesItem[]
    ) { }
}