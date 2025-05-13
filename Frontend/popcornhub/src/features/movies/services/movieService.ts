import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import { env } from '../../../env.schema';
import type { 
    GenresResponse, 
    MovieCommentResponse, 
    MovieCommentsResponse, 
    MovieCreditsResponse, 
    MoviesResponse 
} from '../types/movieTypes';
import { ALL_GENRES } from '../../../constants';


const movieApi = createApi({
    reducerPath: 'movie',
    baseQuery: fetchBaseQuery({
        baseUrl: env.POPCORNHUB_MOVIE_URL,
        credentials: 'include', 
    }),

    endpoints: (builder) => ({
        getMovieGenres: builder.query<GenresResponse, void>({
            query: () => '/movies/genres'
        }),

        searchMovies: builder.query<MoviesResponse, {
            name?: string; genre?: string; sortBy?: 'top' | 'latest', page?: number, pageSize?: number
        }>({
            query: ({ name, genre, sortBy, page, pageSize }) => {
                let url = '/movies?';

                if (name) url += `name=${name}&`;
                if (genre && genre != ALL_GENRES ) url += `genre=${genre}&`;
                if (sortBy) url += `sortBy=${sortBy}&`;
                if (page) url += `page=${page}&`;
                if (pageSize) url += `pageSize=${pageSize}&`;

                return url.slice(0, -1);
            }
        }),

        getMovieComments: builder.query<MovieCommentsResponse, { movieId: number, cursor: number | null, limit: number }>({
            query: ({ movieId, cursor, limit }) => {
                let url = `/movies/${movieId}/comments?limit=${limit}`;
                if (cursor !== null && cursor !== undefined) {
                    url += `&cursor=${cursor}`;
                }
                return url;
            },
        }),

        addMovieComment: builder.mutation<MovieCommentResponse, { movieId: number; comment: string }>({
            query: ({ movieId, comment }) => ({
                url: `/movies/${movieId}/comment`,
                method: 'POST',
                body: { comment },
            }),
        }),

        getMovieCredits: builder.query<MovieCreditsResponse, number>({
            query: (movieId) => `/movies/${movieId}/credits`,
        }),
    }),
});

export const { reducerPath: movieReducerPath, reducer: movieReducer, middleware: movieMiddleware } = movieApi;

export const {
    useGetMovieGenresQuery,
    useLazySearchMoviesQuery,
    useLazyGetMovieCommentsQuery,
    useAddMovieCommentMutation,
    useLazyGetMovieCreditsQuery
} = movieApi;
