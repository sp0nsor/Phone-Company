import {
  ChangeDetectionStrategy,
  Component,
  computed,
  DestroyRef,
  inject,
  OnInit,
  signal,
} from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { SvgIconComponent } from 'angular-svg-icon';
import { catchError, of, tap } from 'rxjs';

import { TariffsService } from '~core/services/tariffs.service';
import { TariffCardComponent } from '~shared/components/tariff-card/tariff-card.component';
import { ITariff } from '~shared/models/tariff.model';

@Component({
  selector: 'app-tariff-plans-section',
  imports: [TariffCardComponent, SvgIconComponent],
  templateUrl: './tariff-plans-section.component.html',
  styleUrl: './tariff-plans-section.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TariffPlansSectionComponent implements OnInit {
  displayedTariffs = computed(() => {
    const list = this.tariffs();
    const start = this.currentIndex();
    const size = this.batchSize;
    if (!list.length) return [];

    const result: ITariff[] = [];

    for (let i = 0; i < size; i++) {
      const index = (start + i) % list.length;
      result.push(list[index]);
    }
    return result;
  });

  private readonly batchSize: number = 3;

  private readonly tariffsService = inject(TariffsService);
  private readonly destroyRef = inject(DestroyRef);

  private tariffs = signal<ITariff[]>([]);
  private currentIndex = signal<number>(0);

  private pageIndex = signal<number>(1);
  private pageCount = signal<number>(0);

  private isLoading: boolean = false;

  ngOnInit(): void {
    this.loadTariffs();
  }

  nextTariff(): void {
    const length = this.tariffs().length;
    if (!length) {
      return;
    }

    const nextIndex = (this.currentIndex() + 1) % length;
    this.currentIndex.set(nextIndex);

    const hasNextPage = this.pageIndex() < this.pageCount();

    console.log(this.pageIndex(), this.pageCount())

    if (hasNextPage) {
      this.loadTariffs();
    }
  }

  prevTariff(): void {
    const length = this.tariffs().length;
    if (!length) {
      return;
    }

    const prevIndex = (this.currentIndex() - 1 + length) % length;

    this.currentIndex.set(prevIndex);
  }

  private loadTariffs(): void {
    if (this.isLoading) {
      return;
    }

    this.isLoading = true;

    this.tariffsService
      .getTariffs(this.pageIndex(), this.batchSize)
      .pipe(
        tap((response) => {
          this.tariffs.update((current) => [...current, ...response.items]);

          this.pageCount.set(response.pageCount);
          this.pageIndex.update((v) => v + 1);

          this.isLoading = false;
        }),
        catchError(() => {
          this.isLoading = false;
          return of([]);
        }),
        takeUntilDestroyed(this.destroyRef),
      )
      .subscribe();
  }
}
