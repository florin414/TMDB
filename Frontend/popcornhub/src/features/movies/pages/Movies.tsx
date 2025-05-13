import { useEffect, useState } from "react";
import GenreFilterDropdown from "../components/GenreFilterDropdown";
import TagOrderDropdown from "../components/TagOrderDropdown";
import { useGetMovieGenresQuery, useLazySearchMoviesQuery } from "../services/movieService";
import styles from './Movies.module.scss';
import LoadingComponent from "../../../shared/components/LoadingComponent";
import ListMovieComponent from "../components/ListMovieComponent";
import SearchBar from "../../../shared/components/SearchBar";
import { ALL_GENRES, DEFAULT_ORDER_BY, DEFAULT_PAGE, DEFAULT_PAGE_SIZE, type OrderBy } from "../../../constants";

const MoviesPage: React.FC = () => {
  const [searchQuery, setSearchQuery] = useState("");
  const [selectedGenre, setSelectedGenre] = useState<string | undefined>(undefined);
  const [selectedSort, setSelectedSort] = useState<OrderBy>(DEFAULT_ORDER_BY);
  const [currentPage, setCurrentPage] = useState(DEFAULT_PAGE);

  const { data: { genres = [] } = {}, isLoading: genresLoading } = useGetMovieGenresQuery();
  const [searchMovies, { data: moviesData, isLoading: moviesLoading }] = useLazySearchMoviesQuery();

  const allGenres = [{ id: 0, name: ALL_GENRES }, ...genres];
  const defaultGenre = allGenres[0];

  useEffect(() => {
    setSelectedSort(DEFAULT_ORDER_BY);
  }, [])

  useEffect(() => {
    searchMovies({
      name: searchQuery,
      genre: selectedGenre,
      sortBy: selectedSort,
      page: currentPage,
      pageSize: DEFAULT_PAGE_SIZE
    });
  }, [searchQuery, selectedGenre, selectedSort, currentPage]);

  const handleSelectGenre = (selectedGenre: { id: number; name: string } | null) => {
    setSelectedGenre(selectedGenre?.name);
  };

  const handleOrderSelect = (order: OrderBy ): void => {
    setSelectedSort(order);
  };

  const handleSearchChange = (newSearchQuery: string) => {
    setSearchQuery(newSearchQuery);
  };

  const handlePageChange = (page: number) => {
    setCurrentPage(page);
  };

  return (
    <div>
      <SearchBar onSearchChange={handleSearchChange} />
      <div className={styles.dropdownContainer}>
        <div>
          <GenreFilterDropdown
            genres={allGenres}
            onSelect={handleSelectGenre}
            loading={genresLoading}
            defaultValue={defaultGenre}
          />
        </div>
        <div>
          <TagOrderDropdown
            onSelect={handleOrderSelect}
            defaultValue={DEFAULT_ORDER_BY}
          />
        </div>
      </div>
      {
        moviesData != undefined
          ? <ListMovieComponent
            moviesData={moviesData}
            currentPage={currentPage}
            onPageChange={handlePageChange}
            pageSize={DEFAULT_PAGE_SIZE}
          />
          : <LoadingComponent />
      }
    </div>
  );
};

export default MoviesPage;