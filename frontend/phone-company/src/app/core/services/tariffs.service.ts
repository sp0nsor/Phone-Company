import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

import { tariffMockData } from '~shared/mocks/tariff.mocks';
import { ITariff } from '~shared/models/tariff.model';

@Injectable({
  providedIn: 'root',
})
export class TariffsService {
  httpClient = inject(HttpClient);
  getTariffs(offset: number, limit: number): Observable<ITariff[]> {
    const tariffs = tariffMockData.slice(offset, offset + limit);
    return of(tariffs);
  }
}
