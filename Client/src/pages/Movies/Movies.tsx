import { useLoaderData } from 'react-router-dom';
import MovieCard from '../../app/components/Cards/MovieCard';
import { Paged } from '../../app/models/paged';
import { Movie } from './models/movieType';
import { MoviesBy } from './api/moviesApi';
import { useEffect, useRef, useState } from 'react';
import Button from '../../app/components/Buttons/Button';
import Spinner from '../../app/components/Spinner';
import { useLazyPagedMoviesQuery } from './api/moviesApiExtended';

function Movies() {
  const moviesData = useLoaderData() as Paged<Movie>;
  const [movies, setMovies] = useState(moviesData.values);
  const [currentPage, setCurrentPage] = useState(moviesData.currentPage);
  const [hasNextPage, setHasNextPage] = useState(moviesData.hasNextPage);
  const [isLoading, setIsLoading] = useState(false);
  const [getPagedMovies] = useLazyPagedMoviesQuery();
  const loaderRef = useRef(null);

  console.log(isLoading);

  useEffect(() => {
    const observer = new IntersectionObserver(
      async ([entry]) => {
        if (
          entry.isIntersecting &&
          hasNextPage &&
          !isLoading &&
          currentPage < 5
        ) {
          setIsLoading(true);
          const res = await getPagedMovies({
            page: currentPage + 1,
            moviesBy: MoviesBy.GetNowPlaying,
          }).unwrap();

          setIsLoading(false);
          setCurrentPage(res.currentPage);
          setHasNextPage(res.hasNextPage);
          setMovies((prev) => [...prev, ...res.values]);
        }
      },
      { root: null, rootMargin: '100px', threshold: 0.1 }
    );

    if (loaderRef.current) {
      observer.observe(loaderRef.current);
    }

    return () => {
      if (loaderRef.current) observer.unobserve(loaderRef.current);
    };
  }, [hasNextPage, isLoading, currentPage, getPagedMovies]);

  console.log(movies);

  // TODO remove loaders, refactor to RTK query
  // TODO add suspense and loading fallback
  // TODO link from movie to movie details

  return (
    <>
      <article className="grid grid-cols-1 gap-y-10 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 2xl:px-40">
        {movies.map((m) => (
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
      <div ref={loaderRef} className="mb-5 mt-10 flex justify-center">
        {isLoading && <Spinner />}
        {currentPage >= 5 && (
          <Button
            onClick={async () => {
              const res = await getPagedMovies({
                page: currentPage + 1,
                moviesBy: MoviesBy.GetNowPlaying,
              }).unwrap();

              setMovies((prev) => [...prev, ...res.values]);
            }}
            className="w-fit px-10"
            isLoading={isLoading}
          >
            Load More
          </Button>
        )}
      </div>
    </>
  );
}
export default Movies;
