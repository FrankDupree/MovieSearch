import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { MovieDetails } from '../models/movie-details.interface';
import { getMovieDetails } from '../api';

const MovieDetailsPage: React.FC = () => {
    const { imdbID } = useParams<{ imdbID: string }>();
    const [movieDetails, setMovieDetails] = useState<MovieDetails | null>(null);
    const navigate = useNavigate(); // Hook to navigate programmatically

    useEffect(() => {
        const fetchMovieDetails = async () => {
            if (imdbID) {
                const details = await getMovieDetails(imdbID);
                setMovieDetails(details);
            }
        };

        fetchMovieDetails();
    }, [imdbID]);

    if (!movieDetails) {
        return <div className="text-center mt-5">Loading...</div>;
    }

    return (
        <div className="container mt-4">
            <div className="card mb-4">
                <div className="card-header bg-primary text-white d-flex align-items-center">
                    <button
                        onClick={() => navigate(-1)}
                        className="btn btn-danger"
                        style={{ marginRight: '10px' }}
                    >
                        Back
                    </button>
                    <h2 className="mb-0">{movieDetails.title} ({movieDetails.year})</h2>
                </div>

                <div className="card-body">
                    <div className="row">
                        <div className="col-md-4">
                            <img
                                src={movieDetails.poster}
                                alt={movieDetails.title}
                                className="img-fluid rounded"
                            />
                        </div>
                        <div className="col-md-8">
                            <p className="lead"><strong>Plot:</strong> {movieDetails.plot}</p>
                            <p><strong>Director:</strong> {movieDetails.director}</p>
                            <p><strong>Actors:</strong> {movieDetails.actors}</p>
                            <p><strong>IMDB Rating:</strong> {movieDetails.imdbRating}</p>
                            <p><strong>Awards:</strong> {movieDetails.awards}</p>
                            <p><strong>Genre:</strong> {movieDetails.genre}</p>
                            <p><strong>Runtime:</strong> {movieDetails.runtime}</p>
                        </div>
                    </div>
                </div>
            </div>
        </div >
    );
};

export default MovieDetailsPage;
