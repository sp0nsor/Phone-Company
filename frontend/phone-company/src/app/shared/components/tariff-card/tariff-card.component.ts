import { ChangeDetectionStrategy, Component, input } from '@angular/core';

import { ITariff } from '~shared/models/tariff.model';

@Component({
  selector: 'app-tariff-card',
  imports: [],
  templateUrl: './tariff-card.component.html',
  styleUrl: './tariff-card.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TariffCardComponent {
  tariff = input<ITariff>();
}
