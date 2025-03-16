import MovieCard from '../../app/components/Cards/MovieCard';
import {
  MoviesBy,
  usePagedMoviesQuery,
  usePagedMoviesSearchByFiltersQuery,
  usePagedMoviesSearchByTitleQuery,
} from './api/moviesApi';
import { useEffect, useState } from 'react';
import Button from '../../app/components/Buttons/Button';
import useScroll from '../../app/hooks/useScroll';
import Spinner from '../../app/components/Spinner';
import ErrorMessage from '../../app/components/ErrorMessage/ErrorMessage';
import { useSearchParams } from 'react-router-dom';
import Filters from './Filters';

export default function Movies() {
  const [page, setPage] = useState(1);
  const { nearBottom } = useScroll();

  const [searchParams] = useSearchParams();
  const search = searchParams.get('search');
  const genreIds = searchParams.getAll('genreIds');

  const { data, isFetching, isError } = usePagedMoviesQuery(
    {
      page,
      moviesBy: MoviesBy.GetNowPlaying,
    },
    { skip: !!search || genreIds.length !== 0 }
  );

  const {
    data: searchedMovies,
    isFetching: isSearchedMoviesFetching,
    isError: isSearchedMoviesError,
  } = usePagedMoviesSearchByTitleQuery(
    {
      page,
      searchTerm: search ?? '',
    },
    { skip: search ? search?.length === 0 : true }
  );

  const {
    data: filteredMovies,
    isFetching: isFilteredMoviesFetching,
    isError: isFilteredMoviesError,
  } = usePagedMoviesSearchByFiltersQuery(
    {
      page,
      genreIds: genreIds,
      year: '2024',
    },
    { skip: genreIds.length === 0 }
  );

  useEffect(() => {
    if (nearBottom) setPage((prev) => (prev > 4 ? prev : prev + 1));
  }, [nearBottom]);

  return (
    <>
      <Filters />
      <article className="mt-2 grid grid-cols-1 gap-y-10 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 2xl:px-40">
        {data?.values.map((m) => (
          <MovieCard
            key={m.id}
            title={m.title}
            id={m.id}
            rating={m.rating}
            releaseDate={m.releaseDate}
            posterPath={m.posterPath}
          />
        ))}
        {searchedMovies?.values.map((m) => (
          <MovieCard
            key={m.id}
            title={m.title}
            id={m.id}
            rating={m.rating}
            releaseDate={m.releaseDate}
            posterPath={m.posterPath}
          />
        ))}
        {filteredMovies?.values.map((m) => (
          <MovieCard
            key={m.id}
            title={m.title}
            id={m.id}
            rating={m.rating}
            releaseDate={m.releaseDate}
            posterPath={m.posterPath}
          />
        ))}
      </article>
      <div className="mb-20 mt-10 flex justify-center">
        {(isFetching || isSearchedMoviesFetching || isFilteredMoviesFetching) &&
          page < 5 && <Spinner />}
        {(isError || isFilteredMoviesError || isSearchedMoviesError) && (
          <ErrorMessage />
        )}
        {page > 4 && (
          <Button
            onClick={() => setPage((prev) => prev + 1)}
            className="w-fit px-10"
            isLoading={isFetching}
          >
            Load More
          </Button>
        )}
      </div>
    </>
  );
}
