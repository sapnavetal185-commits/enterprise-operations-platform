import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Travel } from '../../shared/models/travel';
import { environment } from '../../../environment/environment';

@Injectable({
  providedIn: 'root'
})
export class TravelService {
  private readonly http = inject(HttpClient);

  private readonly apiUrl = `${environment.apiUrl}/Travel`;

  getTrips(): Observable<Travel[]> {
    return this.http.get<Travel[]>(this.apiUrl);
  }

  getTripById(id: number): Observable<Travel> {
    return this.http.get<Travel>(`${this.apiUrl}/${id}`);
  }
}