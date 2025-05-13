import React, { useEffect, useState } from 'react';
import ListCommentsComponent from '../components/ListCommentsComponent';
import MovieViewDetailsComponent from '../components/MovieViewDetailsComponent';
import { useAddMovieCommentMutation, useLazyGetMovieCommentsQuery, useLazyGetMovieCreditsQuery } from '../services/movieService';
import { useSelector } from 'react-redux';
import type { RootState } from '../../../app/store';
import type { Comment } from '../types/movieTypes';
import { DEFAULT_LIMIT } from '../../../constants';

const MovieDetailsPage: React.FC = () => {
  const movie = useSelector((state: RootState) => state.selectedMovie.movie);

  const [allComments, setAllComments] = useState<Comment[]>([]);
  const [cursor, setCursor] = useState<number | null>(null);
  const [actors, setActors] = useState<string[]>([]);

  const [triggerFetch, { data }] = useLazyGetMovieCommentsQuery();
  const [addComment] = useAddMovieCommentMutation();
  const [getMovieCredits, { data: creditsData }] = useLazyGetMovieCreditsQuery()

  useEffect(() => {
    triggerFetch({ movieId: movie.movieId, cursor: null, limit: DEFAULT_LIMIT });
  }, []);

  useEffect(() => {
    if (data) {
      setAllComments((prev) => [...prev, ...data.movieComments]);
      setCursor(data.nextCursor ?? null);
    }
  }, [data]);

  useEffect(() => {
    if (movie?.movieId) {
      getMovieCredits(movie.movieId);
    }
  }, [movie]);

  useEffect(() => {
    if (creditsData && creditsData.credits.cast) {
      const topActors = creditsData.credits.cast
        .slice(0, 5)
        .map((actor) => actor.name);

      setActors(topActors);
    }
  }, [creditsData]);

  const handleAddComment = async (newComment: string) => {
    const response = await addComment({ movieId: movie.movieId, comment: newComment }).unwrap();
    setAllComments(prev => [response.movieComment, ...prev]);
  };

  const handleViewMore = (): void => {
    if (cursor !== null) {
      triggerFetch({ movieId: movie?.movieId, cursor, limit: DEFAULT_LIMIT });
    }
  };

  return (
    <div>
      <MovieViewDetailsComponent
        actors={actors}
        description={movie.description}
        imageUrl={movie.imageUrl}
      />
      <ListCommentsComponent
        comments={allComments}
        hasMore={cursor != null}
        onViewMore={handleViewMore}
        onAddComment={handleAddComment}
      />
    </div>
  );
};

export default MovieDetailsPage;
