import axios from 'axios';
import { MovieDetails } from './models/movie-details.interface';
import { MovieSearchResponse } from './models/movie-search-response.interface';

const API_BASE_URL = 'http://localhost:5193/api/movie';

export const searchMoviesByTitle = async (title: string): Promise<MovieSearchResponse> => {
    const response = await axios.get<MovieSearchResponse>(`${API_BASE_URL}/search`, {
        params: { title }
    });
    return response.data;
};

export const getMovieDetails = async (imdbID: string): Promise<MovieDetails> => {
    const response = await axios.get<MovieDetails>(`${API_BASE_URL}/details`, {
        params: { imdbId: imdbID }
    });
    return response.data;
};

export const getRecentSearches = async () => {
    const response = await axios.get(`${API_BASE_URL}/recent`);
    return response.data; 
};
