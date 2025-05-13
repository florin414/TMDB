import { createBrowserRouter } from 'react-router-dom';
import AuthRouting from '../features/auth/routing';
import MoviesRouting from '../features/movies/routing';
import HomeRouting from '../features/home/routing';

const router = createBrowserRouter([
  HomeRouting(),
  MoviesRouting(),
  AuthRouting(),
]);

export default router;
