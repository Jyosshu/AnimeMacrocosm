import { ProductionStudio } from '../models/productionStudio.model';
import { Distributor } from '../models/distributor.model';
import { CreatorAuthor } from './creatorAuthor.model';
import { Format } from './format.model';

export class SeriesItem {
    constructor (
        public seriesId: number = 0,
        public title: string = '',
        public description: string = '',
        public productionStudios: ProductionStudio[],
        public distributors: Distributor[],
        public creatorAuthors: CreatorAuthor[],
        public length: string = '',
        public format: Format = new Format(),
        public releaseDate: Date = null,
    ) { }
}