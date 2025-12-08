import { useEffect } from 'react';
import ChevronUp from '@/assets/icons/ChevronUp';
import Button from '@/components/Buttons/Button';
import ErrorMessage from '@/components/ErrorMessage/ErrorMessage';
import Spinner from '@/components/Spinner';
import { useInfiniteQuery } from '@tanstack/react-query';
import ToPagedData from '@/utils/toPagedData';
import useScroll from '@/hooks/useScroll';
import { MoviesBy, moviesClient } from '@/features/Movies/api/moviesClient';
import { genres } from '@/assets/tmdbGenres.json';
import GenreButton from '@/components/Buttons/GenreButton';
import { getRouteApi } from '@tanstack/react-router';
import MovieCard from '@/components/Cards/MovieCard';

const { useSearch } = getRouteApi('/movies/');

export default function Movies() {
  const { search, genreIds } = useSearch({ select: (params) => params });

  const isDefaultMovies = !search && !genreIds;
  const isSearchingMovies = search ? search?.length > 0 : false;
  const isFilteringMovies = genreIds && genreIds.length > 0;
  const { nearBottom, beyondScreen } = useScroll();

  const { data, isFetching, isError, fetchNextPage } = useInfiniteQuery({
    queryKey: ['getPagedMovies'],
    queryFn: ({ pageParam }) =>
      moviesClient.getPagedMovies(MoviesBy.GetNowPlaying, pageParam),
    initialPageParam: 1,
    getNextPageParam: (lastPage) => lastPage.currentPage + 1,
    select: (data) => ToPagedData(data),
    enabled: isDefaultMovies,
  });

  const {
    data: searchedMovies,
    isFetching: isSearchedMoviesFetching,
    isError: isSearchedMoviesError,
    fetchNextPage: fetchNextPageByTitle,
  } = useInfiniteQuery({
    queryKey: ['getPagedMoviesSearchByTitle', search],
    queryFn: ({ pageParam }) =>
      moviesClient.getPagedMoviesSearchByTitle(search ?? '', pageParam),
    initialPageParam: 1,
    getNextPageParam: (lastPage) => lastPage.currentPage + 1,
    select: (data) => ToPagedData(data),
    enabled: isSearchingMovies,
  });

  const {
    data: filteredMovies,
    isFetching: isFilteredMoviesFetching,
    isError: isFilteredMoviesError,
    fetchNextPage: fetchNextPageByFilters,
  } = useInfiniteQuery({
    queryKey: ['getPagedMoviesSearchByFilters', genreIds],
    queryFn: ({ pageParam }) =>
      moviesClient.getPagedMoviesSearchByFilters(
        genreIds ?? [],
        `${new Date().getFullYear()}`,
        pageParam
      ),
    initialPageParam: 1,
    getNextPageParam: (lastPage) => lastPage.currentPage + 1,
    select: (data) => ToPagedData(data),
    enabled: isFilteringMovies,
  });

  useEffect(() => {
    if (!nearBottom) return;

    if (isDefaultMovies && data?.hasNextPage && data?.currentPage < 6) {
      fetchNextPage();
    }

    if (
      isFilteringMovies &&
      filteredMovies?.hasNextPage &&
      filteredMovies?.currentPage < 6
    ) {
      fetchNextPageByFilters();
    }

    if (
      isSearchingMovies &&
      searchedMovies?.hasNextPage &&
      searchedMovies?.currentPage < 6
    ) {
      fetchNextPageByTitle();
    }
  }, [
    data?.currentPage,
    data?.hasNextPage,
    fetchNextPage,
    fetchNextPageByFilters,
    fetchNextPageByTitle,
    filteredMovies?.currentPage,
    filteredMovies?.hasNextPage,
    isDefaultMovies,
    isFilteringMovies,
    isSearchingMovies,
    nearBottom,
    searchedMovies?.currentPage,
    searchedMovies?.hasNextPage,
  ]);

  return (
    <>
      <section className="mx-auto flex w-2/3 flex-wrap items-center justify-center">
        {genres?.map((g) => (
          <GenreButton
            key={g.id}
            name={g.name}
            genreId={g.id}
            currentGenreIds={genreIds}
          />
        ))}
      </section>
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
        {(isFetching ||
          isSearchedMoviesFetching ||
          isFilteredMoviesFetching) && <Spinner />}
        {(isError || isFilteredMoviesError || isSearchedMoviesError) && (
          <ErrorMessage />
        )}
        {(isDefaultMovies && data?.currentPage && data?.currentPage > 5) ||
          (isSearchingMovies &&
            searchedMovies?.currentPage &&
            searchedMovies?.currentPage > 5) ||
          (isFilteringMovies &&
            filteredMovies?.currentPage &&
            filteredMovies?.currentPage > 5 && (
              <Button
                onClick={() => {
                  if (isDefaultMovies && data?.hasNextPage) {
                    fetchNextPage();
                  }

                  if (isFilteringMovies && filteredMovies?.hasNextPage) {
                    fetchNextPageByFilters();
                  }

                  if (isSearchingMovies && searchedMovies?.hasNextPage) {
                    fetchNextPageByTitle();
                  }
                }}
                className="w-fit px-10"
                isLoading={
                  isFetching ||
                  isFilteredMoviesFetching ||
                  isSearchedMoviesFetching
                }
              >
                Load More
              </Button>
            ))}
      </div>
      {beyondScreen && (
        <button
          onClick={() => scrollTo(0, 0)}
          className="text-primary hover:outline-foreground active:bg-opacity-80 fixed right-10 bottom-20 animate-bounce rounded-full p-1 hover:outline"
        >
          <ChevronUp />
        </button>
      )}
    </>
  );
}
