export interface Genre {
    id: number;
    name: string;
}

export interface GenresResponse {
    genres: Genre[];
}

export interface SearchMovie {
    id: number;
    title: string;
    overview: string;
    releaseDate: string;
    posterPath: string;
    voteAverage: number;
}

export interface MoviesResponse {
    movies: SearchMovie[];
    totalCount: number,
    page: number,
    pageSize: number
}

export interface Comment {
    id: number;
    moveId: { value: number };
    comment: { value: string };
    createAt: string;
}

export interface MovieCommentsResponse {
    movieComments: Comment[];
    nextCursor: number | null;
}

export interface MovieCommentResponse {
    movieComment: Comment;
}

export interface Actor {
    id: number;
    name: string;
    character: string;
    profilePath: string;
}

export interface MovieCreditsResponse {
    credits: {
      movieId: number;
      cast: {
        id: number;
        name: string;
        character: string;
        profilePath: string;
      }[];
    };
}