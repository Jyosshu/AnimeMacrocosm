import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, from } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { Series } from '../models/series.model';
import { SeriesSummary } from '../models/seriesSummary.model';
import { SeriesItem } from '../models/seriesItem.model';
import { environment } from '../../environments/environment';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class SeriesService {

  constructor(private httpClient: HttpClient) { }

  getAllSeries(): Observable<Series[]> {
    return this.httpClient.get<Series[]>(`${environment.apiEndpoint}/Series/GetAllSeries`)
      // .catch(this.handleError);
  }

  getSeriesById(id: number): Observable<SeriesSummary> {
    return this.httpClient.get<SeriesSummary>(`${environment.apiEndpoint}/Series/GetSeriesById/${id}`)
      // .catch(this.handleError);
  }

  private handleError(error: any) {
    return Observable.throw(error.any);
  }

  // private handleError<T> (operation = 'operation', result?: T)
  // {
  //   return (error: any): Observable<T> => {
  //     console.error(error);
  //     // this.log(`${operation} failed: ${error.message}`);
  //     return of(result as T);
  //   }
  // }
}
