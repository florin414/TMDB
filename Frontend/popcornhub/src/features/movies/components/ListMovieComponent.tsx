import React from 'react';
import { Pagination } from '@mui/material';
import MovieViewCardComponent from './MovieViewCardComponent';
import styles from './ListMovieComponent.module.scss';
import type { MoviesResponse } from '../types/movieTypes';

interface ListMovieComponentProps {
  moviesData: MoviesResponse;
  currentPage: number;
  onPageChange: (page: number) => void;
  pageSize: number;
}

const ListMovieComponent: React.FC<ListMovieComponentProps> = ({
  moviesData, currentPage, onPageChange, pageSize
}) => {
  const pageCount = Math.ceil(moviesData.totalCount / pageSize);

  const handleChange = (_: React.ChangeEvent<unknown>, value: number) => {
    onPageChange(value);
  };

  return (
    <div className={styles.container}>
      <div className={styles.paginationContainer}>
        <Pagination
          count={pageCount}
          page={currentPage}
          onChange={handleChange}
          shape="rounded"
        />
      </div>
      {moviesData.movies.map((movie) => (
        <MovieViewCardComponent
          key={movie.id}
          imageUrl={`https://image.tmdb.org/t/p/w500${movie.posterPath}`}
          title={movie.title}
          description={movie.overview}
          movieId={movie.id}
        />
      ))}
      <div className={styles.paginationContainer}>
        <Pagination
          count={pageCount}
          page={currentPage}
          onChange={handleChange}
          shape="rounded"
        />
      </div>
    </div>
  );
};

export default ListMovieComponent;
