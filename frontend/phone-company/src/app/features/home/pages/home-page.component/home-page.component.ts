import { ChangeDetectionStrategy, Component } from '@angular/core';

import { PromoSectionComponent } from '~features/home/components/promo-section/promo-section.component';
import { TariffPlansSectionComponent } from '~features/home/components/tariff-plans-section/tariff-plans-section.component';

@Component({
  selector: 'app-home-page',
  imports: [PromoSectionComponent, TariffPlansSectionComponent],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HomePageComponent {}
