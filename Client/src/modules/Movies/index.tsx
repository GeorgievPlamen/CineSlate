import { useState } from 'react';
import Button from '@/components/Buttons/Button';
import ErrorMessage from '@/components/ErrorMessage/ErrorMessage';
import Spinner from '@/components/Spinner';
import { useInfiniteQuery } from '@tanstack/react-query';
import ToPagedData from '@/utils/toPagedData';
import { genres } from '@/assets/tmdbGenres.json';
import { getRouteApi } from '@tanstack/react-router';
import MovieCard from '@/components/Cards/MovieCard';
import { moviesClient, MoviesBy, MoviesByTitleMap } from './api/moviesClient';
import { useInfiniteScroll } from '@/hooks/useInfiniteScroll';
import SortByDropdown from './Components/SortByDropdown';
import SectionBreak from '@/components/SectionBreak';
import GenreCheckbox from '@/components/Checkboxes/GenreCheckbox';
import YearSlider from '@/components/Sliders/YearSlider';
import { FilterIcon } from 'lucide-react';

const { useSearch, useNavigate } = getRouteApi('/movies/');

const currentYear = new Date().getFullYear();
const minYear = 1950;

export default function Movies() {
  const { search, genreIds } = useSearch({ select: (params) => params });
  const navigate = useNavigate();
  const [moviesBy, setMoviesBy] = useState<MoviesBy>(MoviesBy.NowPlaying);
  const [years, setYears] = useState([minYear, currentYear]);
  const [isMobileFiltersShown, setIsMobileFiltersShown] = useState(false);

  const hasChangedYears = !(years[0] == minYear && years[1] == currentYear);
  const isSearchingMovies = search ? search?.length > 0 : false;
  const isFilteringMovies =
    (genreIds && genreIds.length > 0) || hasChangedYears;
  const isDefaultMovies = !isSearchingMovies && !isFilteringMovies;

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
    queryKey: ['getPagedMoviesSearchByFilters', genreIds, years],
    queryFn: ({ pageParam }) =>
      moviesClient.getPagedMoviesSearchByFilters(
        genreIds ?? [],
        `${years[0]}`,
        `${years[1]}`,
        pageParam
      ),
    initialPageParam: 1,
    getNextPageParam: (lastPage) => lastPage.currentPage + 1,
    select: (data) => ToPagedData(data),
    enabled: isFilteringMovies,
  });

  async function fetchNext() {
    if (isDefaultMovies && data?.hasNextPage && data?.currentPage < 6) {
      await fetchNextPage();
    }

    if (
      isFilteringMovies &&
      filteredMovies?.hasNextPage &&
      filteredMovies?.currentPage < 6
    ) {
      await fetchNextPageByFilters();
    }

    if (
      isSearchingMovies &&
      searchedMovies?.hasNextPage &&
      searchedMovies?.currentPage < 6
    ) {
      await fetchNextPageByTitle();
    }
  }

  const { loadMoreRef } = useInfiniteScroll(fetchNext);

  function handleSelectMoviesBy(moviesBy: MoviesBy) {
    setMoviesBy(moviesBy);
    navigate({
      to: '/movies',
    });
  }

  const moviesByDropdownItems = [
    <button
      className="w-full cursor-pointer"
      key={MoviesBy.NowPlaying}
      onClick={() => handleSelectMoviesBy(MoviesBy.NowPlaying)}
    >
      {MoviesByTitleMap[MoviesBy.NowPlaying]}
    </button>,
    <button
      className="w-full cursor-pointer"
      key={MoviesBy.Popular}
      onClick={() => handleSelectMoviesBy(MoviesBy.Popular)}
    >
      {MoviesByTitleMap[MoviesBy.Popular]}
    </button>,
    <button
      className="w-full cursor-pointer"
      key={MoviesBy.TopRated}
      onClick={() => handleSelectMoviesBy(MoviesBy.TopRated)}
    >
      {MoviesByTitleMap[MoviesBy.TopRated]}
    </button>,
    <button
      className="w-full cursor-pointer"
      key={MoviesBy.Upcoming}
      onClick={() => handleSelectMoviesBy(MoviesBy.Upcoming)}
    >
      {MoviesByTitleMap[MoviesBy.Upcoming]}
    </button>,
  ];

  return (
    <section className="flex flex-col md:flex-row mt-2">
      <div className="md:hidden flex justify-around">
        <div className="flex items-center gap-2">
          <span className="w-full text-sm text-muted-foreground">
            Sort by:{' '}
          </span>
          <SortByDropdown items={moviesByDropdownItems} moviesBy={moviesBy} />
        </div>
        <button
          className="h-12 w-12 rounded-full hover:bg-primary"
          onClick={() => setIsMobileFiltersShown((x) => !x)}
        >
          <FilterIcon className="m-auto" />
        </button>
        <div
          className={`fixed left-0 z-40 md:hidden flex flex-row items-center
           transition-transform duration-300 ease-in-out
           ${isMobileFiltersShown ? 'translate-x-0' : '-translate-x-full'}
        `}
        >
          <div className="flex flex-col border bg-muted rounded-lg h-fit mt-2 max-h-screen scroll-auto">
            <span className="ml-2 mt-1 text-primary">Genre</span>
            <div className="p-1 px-2 grid grid-cols-2 flex-wrap gap-2 mb-2 w-60">
              {genres?.map((g) => (
                <GenreCheckbox
                  key={g.id}
                  name={g.name}
                  genreId={g.id}
                  currentGenreIds={genreIds}
                />
              ))}
            </div>
            <SectionBreak />
            <div className="p-1 px-2">
              <span className="text-primary">Year</span>
              <YearSlider years={years} setYears={setYears} />
            </div>
          </div>
        </div>
      </div>
      <div className="hidden md:flex">
        <div className="w-50" /> {/*Space for the filters*/}
        <div className="flex flex-col items-center fixed ml-4 mt-2">
          <SortByDropdown items={moviesByDropdownItems} moviesBy={moviesBy} />
          <div className="flex flex-col border bg-muted rounded-lg h-fit mt-2">
            <div className="p-1 px-2 flex flex-col flex-wrap gap-2 mb-2">
              <span className="text-primary">Genre</span>
              {genres?.map((g) => (
                <GenreCheckbox
                  key={g.id}
                  name={g.name}
                  genreId={g.id}
                  currentGenreIds={genreIds}
                />
              ))}
            </div>
            <SectionBreak />
            <div className="p-1 px-2 w-40">
              <span className="text-primary">Year</span>
              <YearSlider years={years} setYears={setYears} />
            </div>
          </div>
        </div>
      </div>
      <div className="flex flex-col w-full">
        <div
          id="movieSection"
          className="mt-2 grid grid-cols-2 gap-y-10 lg:grid-cols-3 xl:grid-cols-4 2xl:grid-cols-5"
        >
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
        </div>
        <div className="my-10 flex justify-center" ref={loadMoreRef}>
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
            filteredMovies?.currentPage > 5) ? (
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
          ) : null}
        </div>
      </div>
    </section>
  );
}
