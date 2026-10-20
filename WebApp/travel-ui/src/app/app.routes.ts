import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'trips',
    pathMatch: 'full'
  },
  {
    path: 'trips',
    loadComponent: () =>
      import('./features/travel-list/travel-list')
        .then(m => m.TravelList)
  },
  {
    path: '**',
    redirectTo: 'trips'
  }
];