import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

import { IPaginatedResponse } from '~shared/models/paginated-response.model';
import { ITariff } from '~shared/models/tariff.model';

@Injectable({
  providedIn: 'root',
})
export class TariffsService {
  private readonly apiUrl: string = 'http://localhost:5091/tariff-plans';
  httpClient = inject(HttpClient);

  getTariffs(pageIndex: number, pageSize: number): Observable<IPaginatedResponse<ITariff>> {
    const params = new HttpParams().set('PageIndex', pageIndex).set('PageSize', pageSize);

    return this.httpClient.get<IPaginatedResponse<ITariff>>(this.apiUrl, { params });
  }
}
