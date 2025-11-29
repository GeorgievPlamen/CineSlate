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
import ChevronUp from '../../app/assets/icons/ChevronUp';

export default function Movies() {
  const [pages, setPages] = useState({ default: 1, search: 1, filter: 1 });
  const { nearBottom, beyondScreen } = useScroll();

  const [searchParams] = useSearchParams();
  const search = searchParams.get('search');
  const genreIds = searchParams.getAll('genreIds');

  const isDefaultMovies =
    (search ? search?.length === 0 : true) && genreIds.length === 0;
  const isSearchingMovies = search ? search?.length > 0 : false;
  const isFilteringMovies = genreIds.length > 0;

  const { data, isFetching, isError } = usePagedMoviesQuery(
    {
      page: pages.default,
      moviesBy: MoviesBy.GetNowPlaying,
    },
    { skip: !isDefaultMovies }
  );

  const {
    data: searchedMovies,
    isFetching: isSearchedMoviesFetching,
    isError: isSearchedMoviesError,
  } = usePagedMoviesSearchByTitleQuery(
    {
      page: pages.search,
      searchTerm: search ?? '',
    },
    { skip: !isSearchingMovies }
  );

  const {
    data: filteredMovies,
    isFetching: isFilteredMoviesFetching,
    isError: isFilteredMoviesError,
  } = usePagedMoviesSearchByFiltersQuery(
    {
      page: pages.filter,
      genreIds: genreIds,
      year: '2024',
    },
    { skip: !isFilteringMovies }
  );

  useEffect(() => {
    if (nearBottom) {
      setPages((prev) => {
        if (isDefaultMovies) {
          return { ...prev, default: prev.default + 1 };
        } else if (isFilteringMovies) {
          return { ...prev, filter: prev.filter + 1 };
        } else {
          return { ...prev, search: prev.search + 1 };
        }
      });
    }
  }, [isDefaultMovies, isFilteringMovies, isSearchingMovies, nearBottom]);

  return (
    <>
      <Filters />
      <article className="mt-2 grid grid-cols-1 gap-y-10 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 2xl:px-40">
        {isDefaultMovies &&
          data?.values.map((m) => (
            <MovieCard
              key={m.id}
              title={m.title}
              id={m.id}
              rating={m.rating}
              releaseDate={m.releaseDate}
              posterPath={m.posterPath}
            />
          ))}
        {isSearchingMovies &&
          searchedMovies?.values.map((m) => (
            <MovieCard
              key={m.id}
              title={m.title}
              id={m.id}
              rating={m.rating}
              releaseDate={m.releaseDate}
              posterPath={m.posterPath}
            />
          ))}
        {isFilteringMovies &&
          filteredMovies?.values.map((m) => (
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
      <div className="mt-10 mb-20 flex justify-center">
        {(isFetching || isSearchedMoviesFetching || isFilteredMoviesFetching) &&
          pages.default < 5 && <Spinner />}
        {(isError || isFilteredMoviesError || isSearchedMoviesError) && (
          <ErrorMessage />
        )}
        {pages.default > 4 && (
          <Button
            onClick={() =>
              setPages((prev) => {
                if (isDefaultMovies) {
                  return { ...prev, default: prev.default + 1 };
                } else if (isFilteringMovies) {
                  return { ...prev, filter: prev.filter + 1 };
                } else {
                  return { ...prev, search: prev.search + 1 };
                }
              })
            }
            className="w-fit px-10"
            isLoading={isFetching}
          >
            Load More
          </Button>
        )}
      </div>
      {beyondScreen && (
        <button
          onClick={() => scrollTo(0, 0)}
          className="text-primary hover:outline-whitesmoke active:bg-opacity-80 fixed right-10 bottom-20 animate-bounce rounded-full p-1 hover:outline hover:outline-1"
        >
          <ChevronUp />
        </button>
      )}
    </>
  );
}
