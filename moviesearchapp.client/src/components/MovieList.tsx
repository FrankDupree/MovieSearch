import React from 'react';
import { Movie } from '../models/movie.interface';
import { Link } from 'react-router-dom';

interface MovieListProps {
    movies: Movie[];
}

const MovieList: React.FC<MovieListProps> = ({ movies }) => {
    return (
        <div className="table-responsive">
            <h2>Search Results</h2>
            <table className='table'>
                <thead>
                    <tr>
                        <th>Poster</th>
                        <th>Title</th>
                        <th>Year</th>
                        <th>Type</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {movies.map((movie) => (
                        // <MovieItem key={movie.imdbID} movie={movie} />
                        <tr key={movie.imdbID}>
                            <td><img src={movie.poster} alt={movie.title} height={100} /></td>
                            <td>{movie.title}</td>
                            <td>{movie.year}</td>
                            <td>{movie.type}</td>
                            <td> <Link to={`/details/${movie.imdbID}`}>More Info</Link></td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div >
    );
};

export default MovieList;
