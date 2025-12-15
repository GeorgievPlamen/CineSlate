import { useEffect, useState } from 'react';
import ChevronUp from '@/assets/icons/ChevronUp';
import Button from '@/components/Buttons/Button';
import ErrorMessage from '@/components/ErrorMessage/ErrorMessage';
import Spinner from '@/components/Spinner';
import { useInfiniteQuery, useMutation } from '@tanstack/react-query';
import ToPagedData from '@/utils/toPagedData';
import useScroll from '@/hooks/useScroll';
import { genres } from '@/assets/tmdbGenres.json';
import GenreButton from '@/components/Buttons/GenreButton';
import { getRouteApi } from '@tanstack/react-router';
import MovieCard from '@/components/Cards/MovieCard';
import { moviesClient, MoviesBy, MoviesByTitleMap } from './api/moviesClient';
import Dropdown from '@/components/Dropdown';
import { ChevronDown } from 'lucide-react';
import { cn } from '@/lib/utils';
import { isAuthenticated } from '@/store/userStore';
import { watchlistsClient } from '../Watchlist/api/watchlistClient';

const { useSearch, useNavigate } = getRouteApi('/movies/');

export default function Movies() {
  const { search, genreIds } = useSearch({ select: (params) => params });
  const navigate = useNavigate();
  const isDefaultMovies = !search && !genreIds;
  const isSearchingMovies = search ? search?.length > 0 : false;
  const isFilteringMovies = genreIds && genreIds.length > 0;
  const { nearBottom, beyondScreen } = useScroll();
  const [moviesBy, setMoviesBy] = useState<MoviesBy>(MoviesBy.NowPlaying);

  const { data, isFetching, isError, fetchNextPage } = useInfiniteQuery({
    queryKey: ['getPagedMovies', moviesBy],
    queryFn: ({ pageParam }) =>
      moviesClient.getPagedMovies(moviesBy, pageParam),
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

  const { mutateAsync: addToWatchlistAsync } = useMutation({
    mutationFn: (id: number) => watchlistsClient.addToWatchlist(id),
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

  function handleSelectMoviesBy(moviesBy: MoviesBy) {
    setMoviesBy(moviesBy);
    navigate({
      to: '/movies',
    });
  }

  async function handleAddToWatchlist(id: number) {
    if (!isAuthenticated()) {
      navigate({ to: '/watchlist' });
      return;
    }

    await addToWatchlistAsync(id);
  }

  const moviesByDropdownItems = [
    <div onClick={() => handleSelectMoviesBy(MoviesBy.NowPlaying)}>
      {MoviesByTitleMap[MoviesBy.NowPlaying]}
    </div>,
    <div onClick={() => handleSelectMoviesBy(MoviesBy.Popular)}>
      {MoviesByTitleMap[MoviesBy.Popular]}
    </div>,
    <div onClick={() => handleSelectMoviesBy(MoviesBy.TopRated)}>
      {MoviesByTitleMap[MoviesBy.TopRated]}
    </div>,
    <div onClick={() => handleSelectMoviesBy(MoviesBy.Upcoming)}>
      {MoviesByTitleMap[MoviesBy.Upcoming]}
    </div>,
  ];

  return (
    <>
      <section className="mx-auto flex w-5/6 md:w-2/3 flex-wrap items-center justify-start">
        <div>
          <span className="text-xs text-muted-foreground">By: </span>
          <Dropdown items={moviesByDropdownItems}>
            <div
              className={cn(
                'flex items-center m-2 h-8 rounded-full px-1',
                isDefaultMovies ? 'bg-primary' : 'bg-muted'
              )}
            >
              <p className="w-20 text-sm">{MoviesByTitleMap[moviesBy]}</p>
              <ChevronDown />
            </div>
          </Dropdown>
        </div>
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
              onAddToWatchlistClick={() => handleAddToWatchlist(m.id)}
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
              onAddToWatchlistClick={() => handleAddToWatchlist(m.id)}
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
              onAddToWatchlistClick={() => handleAddToWatchlist(m.id)}
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
