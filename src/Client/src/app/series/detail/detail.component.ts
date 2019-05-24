import { Component, OnInit } from '@angular/core';
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

  constructor(private seriesService: SeriesService) { }

  ngOnInit() {
  }

  getSeriesById(id): void {
    // this.seriesService.getSeriesById(id).subscribe(SeriesItem => this.seriesItem = SeriesItem);
  }
}
