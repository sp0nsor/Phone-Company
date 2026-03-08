import { ChangeDetectionStrategy, Component } from '@angular/core';
import { SvgIconComponent } from 'angular-svg-icon';

@Component({
  selector: 'app-promo-section',
  imports: [SvgIconComponent],
  templateUrl: './promo-section.component.html',
  styleUrl: './promo-section.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PromoSectionComponent {}
