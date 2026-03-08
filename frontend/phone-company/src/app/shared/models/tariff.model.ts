import { IService } from './service.model';

export interface ITariff {
  id: string;
  name: string;
  description: string;
  price: number;
  services: IService[];
}
