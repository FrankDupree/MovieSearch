import React, { useEffect, useState } from 'react';
import { getRecentSearches, searchMoviesByTitle } from '../api';
import { Movie } from '../models/movie.interface';
import MovieList from './MovieList';
import { toast } from 'react-toastify';

const MovieSearch: React.FC = () => {
    const [query, setQuery] = useState<string>('');
    const [movies, setMovies] = useState<Movie[]>([]);
    const [loading, setLoading] = useState<boolean>(false);
    const [recentSearches, setRecentSearches] = useState<string[]>([]);
    const [searchTriggered, setSearchTriggered] = useState<boolean>(false);

    useEffect(() => {
        const fetchRecentSearches = async () => {
            try {
                const searches = await getRecentSearches();
                setRecentSearches(searches);
            } catch (error) {
                console.error("Error fetching recent searches", error);
            }
        };

        fetchRecentSearches();
    }, []);

    useEffect(() => {
        if (!searchTriggered) return; // Do nothing if search hasn't been triggered

        const handleSearch = async () => {
            setLoading(true);

            try {
                const response = await searchMoviesByTitle(query);
                if (response.response === 'True') {
                    setMovies(response.search);
                    await updateRecentSearches();
                } else {
                    setMovies([]);
                    toast.error('No results found. Please try a different search.');
                }
            } catch (error) {
                console.error('Error fetching movies', error);
                toast.error('An error occurred while searching for movies.');
            }

            setLoading(false);
            setSearchTriggered(false);
        };

        handleSearch();
    }, [searchTriggered]);

    const updateRecentSearches = async () => {
        try {
            const searches = await getRecentSearches();
            setRecentSearches(searches);
        } catch (error) {
            console.error("Error updating recent searches", error);
        }
    };

    const handleRecentSearchClick = async (search: string) => {
        setQuery(search);
    };

    const handleSearchClick = () => {
        if (query.trim() === '') {
            toast.error('Please enter a search term.');
        } else {
            setSearchTriggered(true);
        }
    };

    return (
        <div className="container mt-4 mb-4">
            <div className="card w-100">
                <div className="card">
                    <div className="card-header">
                        Movie Search
                    </div>
                    <div className="card-body">
                        <div className="row mb-3">
                            <div className="col-md-2">Search by name:</div>
                            <div className="col-md-4">
                                <input
                                    type="text"
                                    className="form-control"
                                    value={query}
                                    onChange={(e) => setQuery(e.target.value)}
                                />
                            </div>
                            <div className="col-md-2">
                                <button className="btn btn-primary" onClick={handleSearchClick}>
                                    {loading ? 'Loading...' : 'Search'}
                                </button>
                            </div>
                        </div>
                        {movies.length > 0 && <MovieList movies={movies} />}
                        {recentSearches.length > 0 && (
                            <div className="mt-4">
                                <h5>Recent Searches</h5>
                                <ul className="list-group">
                                    {recentSearches.map((search, index) => (
                                        <li
                                            key={index}
                                            className="list-group-item list-group-item-action"
                                            onClick={() => handleRecentSearchClick(search)}
                                            style={{ cursor: 'pointer' }}
                                        >
                                            {search}
                                        </li>
                                    ))}
                                </ul>
                            </div>
                        )}
                    </div>
                </div>
            </div>
        </div>
    );
};

export default MovieSearch;
