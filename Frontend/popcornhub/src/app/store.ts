import { combineReducers, configureStore } from "@reduxjs/toolkit"
import { authMiddleware, authReducer, authReducerPath } from "../features/auth/services/authServices"
import selectedMovieReducer from "../features/movies/selectedMovieSlice"
import { movieMiddleware, movieReducer, movieReducerPath } from "../features/movies/services/movieService";
import { rtkAuthNotiferMiddleware } from "./middlewares/rtkAuthNotiferMiddleware";
import { rtkMovieNotifierMiddleware } from "./middlewares/rtkMovieNotifierMiddleware";

const rootReducer = combineReducers({
    [movieReducerPath]: movieReducer,
    [authReducerPath]: authReducer,
    selectedMovie: selectedMovieReducer,
});	
  
const store = configureStore({
    reducer: rootReducer,
    middleware: (getDefaultMiddleware) => 
        getDefaultMiddleware()
        .concat(movieMiddleware)
        .concat(authMiddleware)
        .concat(rtkAuthNotiferMiddleware)
        .concat(rtkMovieNotifierMiddleware)
});

export default store;
export type AppDispatch = typeof store.dispatch 
export type RootState = ReturnType<typeof store.getState> 
