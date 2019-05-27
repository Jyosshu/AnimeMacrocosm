import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SeriesSummary } from '../../models/seriesSummary.model';
import { SeriesService } from '../series.service';

@Component({
  selector: 'app-summary',
  templateUrl: './summary.component.html',
  styleUrls: ['./summary.component.css']
})
export class SummaryComponent implements OnInit {

  seriesSummary: SeriesSummary;

  constructor(private seriesService: SeriesService, private route: ActivatedRoute) { }

  ngOnInit() {

    let id = this.route.snapshot.params.id;

    this.getSeriesSummary(id);
  }

  getSeriesSummary(id): void {
    this.seriesService.getSeriesById(id).subscribe(seriesSummary => this.seriesSummary = seriesSummary);
  }

  toArray(seriesSummary: object) {
    return Object.keys(seriesSummary).map(key => seriesSummary[key])
  }
}
