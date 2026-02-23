import { Component } from '@angular/core';
import { PromoSectionComponent } from '~features/home/components/promo-section/promo-section.component';

@Component({
  selector: 'app-home-page',
  imports: [PromoSectionComponent],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.scss',
})
export class HomePageComponent {}
