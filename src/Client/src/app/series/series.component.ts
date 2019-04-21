import { Component, OnInit } from '@angular/core';
import { Series } from '../models/series.model';
import { SeriesService } from '../series/series.service';

@Component({
  selector: 'app-series',
  templateUrl: './series.component.html',
  styleUrls: ['./series.component.css']
})
export class SeriesComponent implements OnInit {

  series: Series[];

  constructor(private seriesService: SeriesService) { }

  ngOnInit() {
    this.getSeries();
  }

  getSeries(): void {
    this.seriesService.getAllSeries().subscribe(series => this.series = series);
  }
}
