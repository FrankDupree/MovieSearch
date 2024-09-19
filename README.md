# Movie Search App

## Application Overview

- **Frontend**: The frontend app runs on [https://localhost:5173](https://localhost:5173).
- **Backend**: The backend runs on [http://localhost:5193/swagger/index.html](http://localhost:5193/swagger/index.html).

## Technology Stack

- **.NET Core Version**: 8
- **React Version**: ^18.3.1


## Application Screenshots

![Screenshot 1](https://github.com/user-attachments/assets/b9d2f9b1-a3ac-4bf7-9fbd-0acde0f8097b)
![Screenshot 2](https://github.com/user-attachments/assets/13c16ce0-e02b-4ec7-9023-dbe69fa10fed)
![Screenshot 3](https://github.com/user-attachments/assets/016efb25-266e-4b5d-a940-a54d0a623442)

## API Endpoints

### 1. Search Movies

- **Endpoint:** `GET /api/movie/search`
- **Parameters:** 
  - `title` (query string): The title of the movie to search for.
- **Response:** Returns a list of movies matching the search title.
- **Errors:** Returns a `400 Bad Request` if the search title is empty or null.

### 2. Get Recent Searches

- **Endpoint:** `GET /api/movie/recent`
- **Response:** Returns a list of recent search titles.

### 3. Get Movie Details

- **Endpoint:** `GET /api/movie/details`
- **Parameters:**
  - `imdbId` (query string): The IMDb ID of the movie to fetch details for.
- **Response:** Returns detailed information about the movie.

## Services

### RecentSearchesService

- **Functionality:** Manages the list of recent search titles.
- **Methods:**
  - `AddSearch(string title)`: Adds a new search title to the recent searches list, maintaining a maximum number of entries.
  - `GetRecentSearches()`: Retrieves the list of recent search titles.

### OMDbService

- **Functionality:** Interacts with the OMDb API to search for movies and retrieve movie details.
- **Methods:**
  - `SearchMovieByTitleAsync(string title)`: Searches for movies by title.
  - `GetMovieDetailsAsync(string imdbId)`: Retrieves movie details by IMDb ID.

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/FrankDupree/MovieSearch.git
   cd MovieSearchApp
   dotnet restore
   dotnet run
