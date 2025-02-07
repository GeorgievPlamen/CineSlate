import MovieCard from '../../app/components/Cards/MovieCard';
import { MoviesBy, usePagedMoviesQuery } from './api/moviesApi';
import { useEffect, useState } from 'react';
import Button from '../../app/components/Buttons/Button';
import useScroll from '../../app/hooks/useScroll';
import Spinner from '../../app/components/Spinner';
import ErrorMessage from '../../app/components/ErrorMessage/ErrorMessage';

export default function Movies() {
  const [page, setPage] = useState(1);
  const { nearBottom } = useScroll();

  const { data, isFetching, isError } = usePagedMoviesQuery({
    page,
    moviesBy: MoviesBy.GetNowPlaying,
  });

  useEffect(() => {
    if (nearBottom) setPage((prev) => (prev > 4 ? prev : prev + 1));
  }, [nearBottom]);

  return (
    <>
      <article className="mt-10 grid grid-cols-1 gap-y-10 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 2xl:px-40">
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
      </article>
      <div className="mb-20 mt-10 flex justify-center">
        {isFetching && page < 5 && <Spinner />}
        {isError && <ErrorMessage />}
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
