import type { RouteObject } from 'react-router-dom';
import Movies from './pages/Movies';
import MovieDetails from './pages/MovieDetails';
import MoviesLayout from '../../layouts/MoviesLayout';

const MoviesRouting = (): RouteObject => ({
  path: '/movies',
  Component: MoviesLayout,
  children: [
    {
      path: '',
      Component: Movies,
    },
    {
      path: ':movieId/details',
      Component: MovieDetails,
    },
  ],
});

export default MoviesRouting;
