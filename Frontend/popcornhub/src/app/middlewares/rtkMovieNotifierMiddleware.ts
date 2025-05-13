import { type Middleware, type MiddlewareAPI, isFulfilled, isRejectedWithValue } from '@reduxjs/toolkit';
import { toast } from 'react-toastify';

type MetaArg = { endpointName: string };

export const rtkMovieNotifierMiddleware: Middleware = (api: MiddlewareAPI) => (next) => (action) => {
  if (isFulfilled(action)) {
    const endpoint = (action.meta?.arg as MetaArg | undefined)?.endpointName;
    
    switch (endpoint) {
      case 'searchMovies':
        toast.success('Movies loaded successfully!');
        break;
      case 'getMovieGenres':
        toast.success('Genres fetched successfully!');
        break;
      case 'getMovieComments':
        toast.success('Comments loaded successfully!');
        break;
      case 'addMovieComment':
        toast.success('Comment added successfully!');
        break;
      case 'getMovieCredits':
        toast.success('Credits fetched successfully!');
        break;
      default:
        break;
    }
  }

  if (isRejectedWithValue(action)) {
    const endpoint = (action.meta?.arg as MetaArg | undefined)?.endpointName;

    switch (endpoint) {
      case 'searchMovies':
        toast.error('Failed to load movies. Please try again later.');
        break;
      case 'getMovieGenres':
        toast.error('Failed to fetch genres. Please try again later.');
        break;
      case 'getMovieComments':
        toast.error('Failed to load comments. Please try again later.');
        break;
      case 'addMovieComment':
        toast.error('Failed to add comment. Please try again later.');
        break;
      case 'getMovieCredits':
        toast.error('Failed to fetch credits. Please try again later.');
        break;
      default:
        toast.error('An unexpected error occurred. Please try again later.');
    }
  }

  return next(action);
};
