
export class SeriesItem {
    constructor (
        public seriesId: number = 0,
        public title: string = '',
        public description: string = '',
        public length: string = '',
        public formatId: number = 0,
        public releaseDate: Date = null,
    ) { }
}