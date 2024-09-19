import { Movie } from "./movie.interface";

export interface MovieSearchResponse {
    search: Movie[];
    totalResults: string;
    response: string;
}
