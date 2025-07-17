# Hotel management

## Overview

This is a simple and efficient .NET API for managing hotels.
It supports CRUD (Create, Read, Update, Delete) operations on hotel entities
and includes advanced search functionality that ranks hotels based on a weighted combination
of price and proximity to a given location.

## Features

- Manage hotels with full CRUD operations via HTTP API:
  - Create new hotel entries
  - Read individual or all hotels
  - Update existing hotel information
  - Delete hotels by ID

- Search hotels with ranking based on:
  - Price weight
  - Distance weight from current location

- Pagination support on hotel search results

## Technologies

- .NET 9.0
- ASP.NET Core Web API
- C# 13.0

## Configuration

The application uses a configuration section `HotelSearchWeights` to define search scoring weights, for example:

```json
 {
  "HotelSearchWeights": {
    "PriceWeight": 0.5,
    "DistanceWeight": 0.5
  }
}
 ```
The sum of weights should not be zero, as the search functionality calculates scores based on these values.

## Getting Started

### Prerequisites

- .NET 9.0 SDK or later
- Any IDE supporting .NET, like Visual Studio 2022 or JetBrains Rider

### Running the Application

1. Clone the repository
2. Adjust the `appsettings.json` if needed
3. Build and run the project
4. Use API endpoints to manage and search hotels

## API Endpoints

- `GET /hotels`  
  Retrieves all hotels

- `GET /hotels/{id}`  
  Retrieve a hotel by its unique ID

- `POST /hotels`  
  Create a new hotel  
  Body example:
  ```json
  {
    "name": "Hotel Name",
    "price": 100,
    "latitude": 45.0,
    "longitude": 15.0
  }
  ```

- `PUT /hotels/{id}`  
  Update an existing hotel by ID  
  Body example (same as create):
  ```json
  {
    "name": "Updated Hotel Name",
    "price": 120,
    "latitude": 45.0,
    "longitude": 15.0
  }
  ```

- `DELETE /hotels/{id}`  
  Delete a hotel by ID

- `GET /hotels/search`  
  Search hotels with pagination and ranking  
  Query parameters:
  - `CurrentLatitude` (double): Current location latitude (required, non-zero)
  - `CurrentLongitude` (double): Current location longitude (required, non-zero)
  - `PageNumber` (int): Page number for pagination (optional, default 1)
  - `PageSize` (int): Number of results per page (optional, default 10)

## Error Handling

- Returns 404 Not Found if the hotel with specified ID is not found
- Throws `PriceAndDistanceWeightZeroException` if both search weights are zero, resulting in a 422 response

## Testing

The repository contains unit tests covering the service layer and repository methods for CRUD operations,
search functionality including sorting and pagination, and error handling scenarios.

---