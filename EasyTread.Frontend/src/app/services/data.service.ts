import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { CrossingClient, CrossingResponse } from 'src/app/domain/client';

import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class DataService {
  constructor(private crossingClient: CrossingClient) {}

  fetchData(): Observable<CrossingResponse[]> {
    return this.crossingClient.getAllCrossingVehicles();
  }

  fetchDataReversed(): Observable<CrossingResponse[]> {
    return this.reverseData(this.fetchData());
  }

  reverseData(
    data$: Observable<CrossingResponse[]>
  ): Observable<CrossingResponse[]> {
    return data$.pipe(map((data) => data.slice().reverse()));
  }
}
