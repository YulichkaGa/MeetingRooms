
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Room {
  id: number;
  name: string;
}

export interface Booking {
  id: string;
  roomId: number;
  title: string;
  start: string;
  end: string;
  bookedBy: string;
}

export type CreateBookingRequest = Omit<Booking, 'id'>;

@Injectable({
  providedIn: 'root'
})
export class BookingService {
  private readonly apiUrl = '/api';

  constructor(private http: HttpClient) {}

  getRooms(): Observable<Room[]> {
    return this.http.get<Room[]>(`${this.apiUrl}/rooms`);
  }

  getBookings(roomId: number, date: string): Observable<Booking[]> {
    return this.http.get<Booking[]>(`${this.apiUrl}/bookings`, {
      params: { roomId, date }
    });
  }

  createBooking(request: CreateBookingRequest): Observable<Booking> {
    return this.http.post<Booking>(`${this.apiUrl}/bookings`, request);
  }

  cancelBooking(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/bookings/${id}`);
  }
}
