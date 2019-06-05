import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SeriesService } from '../series.service';
import { SeriesItem } from '../../models/seriesItem.model';
import { from } from 'rxjs';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css']
})
export class DetailComponent implements OnInit {

  seriesItem: SeriesItem;

  constructor(private seriesService: SeriesService, private route: ActivatedRoute) { }

  ngOnInit() {
    let id = this.route.snapshot.params.id;

    this.getSeriesItemById(id);
  }

  getSeriesItemById(id): void {
    this.seriesService.getSeriesItemById(id).subscribe(SeriesItem => this.seriesItem = SeriesItem);
  }
}
