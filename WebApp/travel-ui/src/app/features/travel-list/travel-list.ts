import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { Travel } from '../../shared/models/travel';
import { TravelService } from '../../core/services/travel-service';
import { Router } from '@angular/router';

type SortOption = 'name' | 'price-low' | 'price-high';

@Component({
  selector: 'app-travel-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './travel-list.html',
  styleUrl: './travel-list.css'
})
export class TravelList implements OnInit {
  private readonly travelService = inject(TravelService);
private readonly router = inject(Router);
  trips: Travel[] = [];

  searchText = '';
  maxPrice: number | null = null;
  minDuration: number | null = null;
  sortBy: SortOption = 'name';

  isLoading = false;
  errorMessage = '';

  ngOnInit(): void {
    this.loadTrips();
  }

  loadTrips(): void {
    this.isLoading = true;
    this.errorMessage = '';

    this.travelService.getTrips().subscribe({
      next: (data: Travel[]) => {
        this.trips = data;
        this.isLoading = false;
      },
      error: (error: unknown) => {
        console.error('Error fetching trips:', error);

        this.errorMessage =
          'Unable to load trips. Please check the API connection and try again.';

        this.isLoading = false;
      }
    });
  }

  get filteredTrips(): Travel[] {
    const search = this.searchText.trim().toLowerCase();

    const filtered = this.trips.filter(trip => {
      const matchesSearch =
        !search ||
        trip.title.toLowerCase().includes(search) ||
        trip.destination.toLowerCase().includes(search);

      const matchesPrice =
        this.maxPrice === null ||
        trip.price <= this.maxPrice;

      const matchesDuration =
        this.minDuration === null ||
        trip.durationDays >= this.minDuration;

      return matchesSearch && matchesPrice && matchesDuration;
    });

    return [...filtered].sort((a, b) => {
      switch (this.sortBy) {
        case 'price-low':
          return a.price - b.price;

        case 'price-high':
          return b.price - a.price;

        case 'name':
        default:
          return a.title.localeCompare(b.title);
      }
    });
  }

  clearFilters(): void {
    this.searchText = '';
    this.maxPrice = null;
    this.minDuration = null;
    this.sortBy = 'name';
  }

  trackById(_index: number, trip: Travel): number {
    return trip.id;
  }
  viewTrip(id: number): void {
  this.router.navigate(['/trips', id]);
}
}