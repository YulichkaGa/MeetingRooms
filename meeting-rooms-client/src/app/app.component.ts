
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {
  BookingService,
  Booking,
  Room
} from './services/booking.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  rooms: Room[] = [];
  bookings: Booking[] = [];

  selectedRoomId = 1;
  selectedDate = new Date().toLocaleDateString('en-CA');

  errorMessage = '';
  showForm = false;

  newBooking = {
    title: '',
    bookedBy: '',
    startTime: '',
    endTime: ''
  };

  constructor(private bookingService: BookingService) {}

  ngOnInit(): void {
    this.bookingService.getRooms().subscribe({
      next: rooms => {
        this.rooms = rooms;

        if (rooms.length > 0) {
          this.selectedRoomId = rooms[0].id;
          this.loadBookings();
        }
      },
      error: err => {
        this.errorMessage =
          err.error?.message || 'Failed to load rooms.';
      }
    });
  }

  loadBookings(): void {
    this.errorMessage = '';

    this.bookingService
      .getBookings(this.selectedRoomId, this.selectedDate)
      .subscribe({
        next: bookings => {
          this.bookings = bookings;
        },
        error: err => {
          this.errorMessage =
            err.error?.message || 'Failed to load bookings.';
        }
      });
  }

  saveBooking(): void {
    const request = {
      roomId: this.selectedRoomId,
      title: this.newBooking.title,
      bookedBy: this.newBooking.bookedBy,
      start: `${this.selectedDate}T${this.newBooking.startTime}:00`,
      end: `${this.selectedDate}T${this.newBooking.endTime}:00`
    };

    this.errorMessage = '';

    this.bookingService.createBooking(request).subscribe({
      next: () => {
        this.showForm = false;
        this.resetForm();
        this.loadBookings();
      },
      error: err => {
        this.errorMessage =
          err.error?.message || 'Failed to create booking.';
      }
    });
  }

  cancelBooking(id: string): void {
    this.errorMessage = '';

    this.bookingService.cancelBooking(id).subscribe({
      next: () => this.loadBookings(),
      error: err => {
        this.errorMessage =
          err.error?.message || 'Failed to cancel booking.';
      }
    });
  }

  resetForm(): void {
    this.newBooking = {
      title: '',
      bookedBy: '',
      startTime: '',
      endTime: ''
    };
  }

  closeForm(): void {
    this.showForm = false;
    this.errorMessage = '';
    this.resetForm();
  }
}
