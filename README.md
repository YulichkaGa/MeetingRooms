
# Meeting Room Booking System

A full-stack meeting room booking application built with ASP.NET Core 8 (C#) and Angular 19 (TypeScript).

The application allows users to view, create, and cancel meeting room bookings while preventing scheduling conflicts.

## Tech Stack

- Backend: ASP.NET Core 8, C#, REST API
- Frontend: Angular 19, TypeScript, SCSS
- Storage: In-memory
- Architecture: Controllers, Services, DTOs, Angular Services

## Prerequisites

- .NET 8 SDK
- Node.js and npm

## Getting Started

### 1. Run the Backend

Open a terminal in the project root:


cd MeetingRooms.Api
dotnet run


### 2. Run the Frontend

Open another terminal in the project root:

bash
cd meeting-rooms-client
npm install
npm start


Open http://localhost:4200 in your browser.

The Angular development server uses `proxy.conf.json` to forward `/api` requests to `https://localhost:7203`.

Make sure the backend is running on the configured HTTPS port.

## Features

- View predefined meeting rooms.
- View bookings by selected room and date.
- Display bookings sorted by start time.
- Create bookings with title, organizer, start time, and end time.
- Cancel existing bookings.
- Validate required fields and booking time ranges.
- Prevent overlapping bookings in the same room.
- Allow back-to-back bookings.
- Display validation and API errors in the UI.

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/rooms` | Retrieve meeting rooms |
| GET | `/api/bookings?roomId=1&date=2026-10-27` | Retrieve bookings by room and date |
| POST | `/api/bookings` | Create a booking |
| DELETE | `/api/bookings/{id}` | Cancel a booking |

### HTTP Status Codes

- 201 Created - Booking created successfully.
- 204 No Content - Booking deleted successfully.
- 400 Bad Request - Invalid booking data.
- 404 Not Found - Booking not found.
- 409 Conflict - Booking overlaps with an existing booking.

## Technical Decisions

### Separation of Concerns

Controllers handle HTTP requests, while a dedicated service manages booking operations, validation, and conflict detection.

### Overlap Detection

Two bookings overlap when:


existing.Start < requested.End && requested.Start < existing.End


This prevents scheduling conflicts while allowing consecutive bookings.

### Concurrency Handling

A lock protects booking operations against concurrent conflicting requests within a single application instance.

### In-Memory Storage

Bookings are stored in memory to keep the implementation simple and focused on the assignment requirements.

### Frontend Integration

An Angular service handles API communication, while the development proxy forwards requests to the backend.

## Testing

The following scenarios were tested manually:

- Successful booking creation.
- Booking cancellation.
- Prevention of overlapping bookings.
- Acceptance of consecutive bookings.
- Validation of invalid time ranges.
- Validation of required fields.
- Filtering bookings by room and date.
- Sorting bookings by start time.
- Displaying API errors.

## Known Limitations

- Booking data is lost when the backend restarts.
- Meeting rooms are predefined.
- In-memory concurrency protection applies to a single backend instance.
- Automated tests were not implemented.

## What I Would Improve With Two More Hours

- Add automated tests for booking validation and conflict detection.
- Add database persistence instead of in-memory storage.

## Development Time

Approximately 4 hours, including implementation, debugging, manual testing, and documentation.
