import { createSlice, type PayloadAction } from "@reduxjs/toolkit";

export interface MovieDetails {
    imageUrl: string;
    title: string;
    description: string;
    movieId: number;
}

interface SelectedMovieState {
    movie: MovieDetails;
}

const initialState: SelectedMovieState = {
    movie: {
        imageUrl: '',
        title: '',
        description: '',
        movieId: 0
    }
};

const selectedMovieSlice = createSlice({
    name: 'selectedMovie',
    initialState,
    reducers: {
        setSelectedMovie(state, action: PayloadAction<MovieDetails>) {
            state.movie = action.payload;
        }
    },
});

export const { setSelectedMovie } = selectedMovieSlice.actions;
export default selectedMovieSlice.reducer;