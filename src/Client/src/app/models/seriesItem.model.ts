import { ProductionStudio } from '../models/productionStudio.model';
import { Distributor } from '../models/distributor.model';

export class SeriesItem {
    constructor (
        public seriesId: number = 0,
        public title: string = '',
        public description: string = '',
        public productionStudio: ProductionStudio = new ProductionStudio(),
        public distributor: Distributor = new Distributor(),
        public length: string = '',
        public formatId: number = 0,
        public formatName: string = '',
        public releaseDate: Date = null,
    ) { }
}