import React from 'react';
import { Link } from 'react-router-dom';
import { Movie } from '../models/movie.interface';

interface MovieItemProps {
    movie: Movie;
}

const MovieItem: React.FC<MovieItemProps> = ({ movie }) => {
    return (
        <div className="movie-item">
            <img src={movie.poster} alt={movie.title} />
            <div>
                <h3>{movie.title}</h3>
                <p>{movie.year}</p>
                <Link to={`/details/${movie.imdbID}`}>More Info</Link>
            </div>
        </div>
    );
};

export default MovieItem;
