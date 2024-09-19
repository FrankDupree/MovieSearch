import { BrowserRouter as Router, Route, Routes } from 'react-router-dom'

import './App.css';
import MovieDetailsPage from './components/MovieDetailsPage';
import MovieSearch from './components/MovieSearch';

const App: React.FC = () => {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<MovieSearch />} />
                <Route path="/details/:imdbID" element={<MovieDetailsPage />} />
            </Routes>
        </Router>
    );
};

export default App;